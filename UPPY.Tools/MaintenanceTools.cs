using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Model;
using Core.Security;
using Mongo.Common;
using Mongo.Common.MongoAdditional.Service;
using MongoDB.Driver;
using UPPY.DataBase.Mongo.DataManager.Drawings;

namespace UPPY.Tools
{
    public delegate void ProgressChangedEventHandler(string collName, int countAll, int countProcessed);

    public class MaintenanceTools
    {
        public ProgressChangedEventHandler ProgressChanged { get; set; }

        sealed class TicketAutUser : ITicketAutUser
        {
            public TicketAutUser(string login, string role, string fio, string ticket)
            {
                Role = role;
                Login = login;
                FIO = fio;
                Ticket = ticket;
            }

            public TicketAutUser(string login, string role, string ticket)
            {
                Role = role;
                Ticket = ticket;
                Login = login;
                FIO = string.Empty;
            }

            public TicketAutUser(string login, string ticket)
            {
                Role = string.Empty;
                Login = login;
                Ticket = ticket;
                FIO = string.Empty;
            }

            public TicketAutUser()
            {
            }

            public string Login { get; set; } = "AdminTools";

            public string FIO { get; set; }

            public string Role { get; set; }
            public string Ticket { get; set; }
        }

        private readonly MongoDbConnection _connection;
        private Database _database;

        public MaintenanceTools()
        {
            var conStr = ConfigurationManager.ConnectionStrings["mongoServer"].ToString();
            var dbName = ConfigurationManager.ConnectionStrings["dbName"].ToString();

            _connection = new MongoDbConnection(conStr, dbName);
            _database = new Database(new TicketAutUser(string.Empty, string.Empty), _connection);
        }

        public List<GrouppedTechRoutes> CheckDeletedTechOpers()
        {
            var result = new List<GrouppedTechRoutes>();

            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            var dmTechOpers = new MongoDbDataManager<TechOperation>(_database);
            var orders = new MongoDbDataManager<Order>(_database).GetListCollection();

            var parentsIds = GetParentIds().ToList();

            var techRoutes = dmTechRoutes.GetListCollection();
            var techOpers = dmTechOpers.GetListCollection();
            var dictDrawings = new Dictionary<int, List<Drawing>>();
            var dictOrders = new List<Order>();

            foreach (var techRoute in techRoutes)
            {
                var actualTechOpers = new List<TechOperation>();

                foreach (var actualTo in techRoute.TechOperations.Select(techOperation => techOpers.FirstOrDefault(x => x.Id == techOperation.Id)))
                {
                    if (actualTo != null)
                        actualTechOpers.Add(actualTo);
                    else
                    {
                        throw new Exception();
                    }
                }

                actualTechOpers = actualTechOpers.OrderBy(x => x.OrderInPrint).ToList();

                if (actualTechOpers.Any(x => x.IsDelete))
                {
                    foreach (var parentsId in parentsIds)
                    {
                        if (!dictDrawings.ContainsKey(parentsId))
                        {
                            var coll = new SepDrawingDataManager(_database, parentsId);
                            dictDrawings.Add(parentsId, coll.GetListCollection());
                        }

                        if (dictDrawings[parentsId].Any(x => x.TechRouteId == techRoute.Id))
                        {
                            var ss = orders.FirstOrDefault(x => x.DrawingId == parentsId);
                            if (dictOrders.All(x => x.Id != ss.Id))
                            {
                                dictOrders.Add(ss);
                                Debug.WriteLine($"Tech route {techRoute.Name}. Order no: {ss.OrderNo}");
                            }
                        }

                        result.Add(new GrouppedTechRoutes() { Name = techRoute.Name, Count = parentsId });
                    }
                }
            }

            foreach (var dictOrder in dictOrders)
            {
                Debug.WriteLine(dictOrder.OrderNo);
            }

            return result;
        }

        /// <summary>
        ///     Проверяет наменования тех. маршрутов в соответсвии с порядком тех. операций
        /// </summary>
        public List<TechRoute> CheckNameTechRoutes()
        {
            var result = new List<TechRoute>();

            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            var dmTechOpers = new MongoDbDataManager<TechOperation>(_database);

            var techRoutes = dmTechRoutes.GetListCollection();
            var techOpers = dmTechOpers.GetListCollection();

            foreach (var techRoute in techRoutes)
            {
                var actualTechOpers = new List<TechOperation>();

                foreach (var actualTo in techRoute.TechOperations.Select(techOperation => techOpers.FirstOrDefault(x => x.Id == techOperation.Id)))
                {
                    if (actualTo != null)
                        actualTechOpers.Add(actualTo);
                    else
                    {
                        throw new Exception();
                    }
                }

                actualTechOpers = actualTechOpers.OrderBy(x => x.OrderInPrint).ToList();

                var oldName = techRoute.Name;
                var oldPositions = techRoute.TechOperations;

                techRoute.TechOperations = actualTechOpers;
                techRoute.CreateNameByTechOperations();

                if (oldName != techRoute.Name)
                {
                    techRoute.TechOperations = oldPositions;
                    techRoute.Name = oldName;
                    result.Add(techRoute);
                }
            }

            return result;
        }

        public List<GrouppedTechRoutes> CheckDoublesTechRoutes()
        {
            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            var techRoutes = dmTechRoutes.GetListCollection();
            var groupped = techRoutes.GroupBy(x => x.Name).Select(x => new GrouppedTechRoutes { Name = x.Key, Count = x.Count() }).Where(x => x.Count > 1);
            return groupped.ToList();
        }

        public List<TechRoute> CheckUnusableTechRoutes()
        {
            var result = new List<TechRoute>();

            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            var techRoutes = dmTechRoutes.GetListCollection();

            var parentsIds = GetParentIds().ToList();

            var dictDrawings = new Dictionary<int, List<Drawing>>();
            var collStandart = new MongoDbDataManager<StandartDrawing>(_database);
            var stCollection = collStandart.GetListCollection();

            foreach (var techRoute in techRoutes)
            {
                var usage = false;

                foreach (var parentsId in parentsIds)
                {
                    if (!dictDrawings.ContainsKey(parentsId))
                    {
                        var coll = new SepDrawingDataManager(_database, parentsId);
                        dictDrawings.Add(parentsId, coll.GetListCollection());
                    }

                    usage = dictDrawings[parentsId].Any(x => x.TechRouteId == techRoute.Id);
                    if (usage)
                        break;
                }

                usage = usage || stCollection.Any(x => x.TechRouteId == techRoute.Id);

                if (!usage)
                    result.Add(techRoute);
            }

            return result;
        }

        public List<GrouppedTechRoutes> CheckUnusableCollections()
        {
            var result = new List<GrouppedTechRoutes>();

            var dmOrders = new MongoDbDataManager<Order>(_database);
            var colls = _connection.Database.ListCollectionsAsync().Result.ToListAsync().Result;
            var collsDraws = colls.Where(y => y.Values.Any(z => z.IsString && z.AsString.StartsWith("drawings_"))).ToList();
            foreach (var collsDraw in collsDraws)
            {
                var name = collsDraw.Values.FirstOrDefault(z => z.IsString && z.AsString.StartsWith("drawings_")).AsString;
                var parentId = Convert.ToInt32(name.Replace("drawings_", string.Empty));
                var collDraw = _connection.Database.GetCollection<Drawing>(name);

                if (dmOrders.GetListCollection().All(x => x.DrawingId != parentId))
                {
                    result.Add(new GrouppedTechRoutes() { Count = (int)collDraw.CountAsync(_ => true).Result, Name = name });
                }
            }

            return result;
        }

        public List<BadHierarchicalData> CheckHierarchicalData()
        {
            var result = new List<BadHierarchicalData>();

            var colls = _connection.Database.ListCollectionsAsync().Result.ToListAsync().Result;
            var collsDraws = colls.Where(y => y.Values.Any(z => z.IsString && z.AsString.StartsWith("drawings_"))).ToList();
            foreach (var collsDraw in collsDraws)
            {
                var name = collsDraw.Values.FirstOrDefault(z => z.IsString && z.AsString.StartsWith("drawings_")).AsString;
                var parentId = Convert.ToInt32(name.Replace("drawings_", string.Empty));
                var collDraw = _connection.Database.GetCollection<Drawing>(name);
                var drawings = collDraw.FindAsync(_ => true).Result.ToListAsync().Result;
                var allId = drawings.Select(x => x.Id);
                var allParentId = drawings.Select(x => x.ParentId).Where(x => x.HasValue).Distinct();


                result.AddRange(allParentId.Where(x => !allId.Contains(x)).Select(id => new BadHierarchicalData() { IdCollection = parentId, IdRecord = id ?? 0, Name = name + " Id: " + (id?.ToString() ?? "<Empty>") }));
            }

            return result;
        }

        public void RepairNameTechRoutes(List<TechRoute> toRepair)
        {
            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            var dmTechOpers = new MongoDbDataManager<TechOperation>(_database);

            var techOpers = dmTechOpers.GetListCollection();

            foreach (var techRoute in toRepair)
            {
                var actualTechOpers = new List<TechOperation>();

                foreach (var actualTo in techRoute.TechOperations.Select(techOperation => techOpers.FirstOrDefault(x => x.Id == techOperation.Id)))
                {
                    if (actualTo != null)
                        actualTechOpers.Add(actualTo);
                    else
                    {
                        throw new Exception();
                    }
                }

                actualTechOpers = actualTechOpers.OrderBy(x => x.OrderInPrint).ToList();

                techRoute.TechOperations = actualTechOpers;
                techRoute.CreateNameByTechOperations();

                dmTechRoutes.Update(techRoute);
            }
        }

        public void RepairDoublesTechRoutes(List<GrouppedTechRoutes> toRepair)
        {
            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);
            Debug.WriteLine(DateTime.Now);
            var techRoutes = dmTechRoutes.GetListCollection().Where(x => toRepair.Any(y => y.Name == x.Name)).OrderBy(x => x.Id).ToList();
            var parentsIds = GetParentIds().ToList();

            var listNormalTechroutes = new List<TechRoute>();
            var collStandart = new MongoDbDataManager<StandartDrawing>(_database);
            var stCollection = collStandart.GetListCollection();
            var listTasks = new List<Task>();

            foreach (var parentsId in parentsIds)
            {
                var task = new Task(() =>
                {
                    var coll = new SepDrawingDataManager(_database, parentsId);
                    var collection = coll.GetListCollection();
                    ProgressChanged?.Invoke($"DrawingId: {parentsId}", parentsIds.Count, parentsIds.IndexOf(parentsId));
                    Debug.WriteLine($"DrawingId: {parentsId}. ID #: {parentsIds.Count}. Index: {parentsIds.IndexOf(parentsId)}");
                    foreach (var group in toRepair.AsParallel())
                    {
                        ProgressChanged?.Invoke(group.Name, toRepair.Count, toRepair.IndexOf(group));
                        var techRoute = techRoutes.FirstOrDefault(x => x.Name == group.Name);
                        if (techRoute != null)
                        {
                            var otherRoutes = techRoutes.Where(x => x.Name == group.Name && x.Id != techRoute.Id);
                            foreach (var drawing in collection.Where(x => otherRoutes.Any(y => x.TechRouteId == y.Id)))
                            {
                                Debug.WriteLine($"Change drawings Id: {drawing.Id}. Old tech route Id {drawing.TechRouteId}. New tech route Id: {techRoute.Id}.");
                                ProgressChanged?.Invoke(group.Name, toRepair.Count, toRepair.IndexOf(group));
                                drawing.TechRouteId = techRoute.Id;
                                coll.Update(drawing);
                            }

                            lock (listNormalTechroutes)
                            {
                                if (listNormalTechroutes.All(x => x.Id != techRoute.Id))
                                    listNormalTechroutes.Add(techRoute);
                            }

                        }

                    }
                });

                task.Start();
                listTasks.Add(task);
            }

            while (!Task.WaitAll(listTasks.ToArray(), 10000))
            {

            }



            foreach (var group in toRepair)
            {
                ProgressChanged?.Invoke(group.Name, toRepair.Count, toRepair.IndexOf(group));
                var techRoute = techRoutes.FirstOrDefault(x => x.Name == group.Name);
                if (techRoute != null)
                {
                    var otherRoutes = techRoutes.Where(x => x.Name == group.Name && x.Id != techRoute.Id);

                    foreach (var standart in stCollection.Where(x => otherRoutes.Any(y => x.TechRouteId == y.Id)))
                    {
                        Debug.WriteLine($"Change standart drawings Id: {standart.Id}. Old tech route Id {standart.TechRouteId}. New tech route Id: {techRoute.Id}.");
                        standart.TechRouteId = techRoute.Id;
                        collStandart.Update(standart);
                    }

                    if (listNormalTechroutes.All(x => x.Id != techRoute.Id))
                        listNormalTechroutes.Add(techRoute);
                }

            }

            foreach (var listNormalTechroute in listNormalTechroutes)
            {
                var otherRoutes = techRoutes.Where(x => x.Name == listNormalTechroute.Name && x.Id != listNormalTechroute.Id);

                foreach (var otherRoute in otherRoutes)
                {
                    Debug.WriteLine($"Delete techroute {otherRoute.Id} {otherRoute.Name}");
                    dmTechRoutes.Delete(otherRoute);
                }
            }

            Debug.WriteLine(DateTime.Now);
        }

        public void DelUnusableTechRoutes(List<TechRoute> toRepair)
        {
            var dmTechRoutes = new MongoDbDataManager<TechRoute>(_database);

            foreach (var techRoute in toRepair)
            {
                Debug.WriteLine($"Delete techroute {techRoute.Id} {techRoute.Name}");
                dmTechRoutes.Delete(techRoute);
            }
        }

        public void DelUnusableCollections(List<GrouppedTechRoutes> toRepair)
        {
            foreach (var coll in toRepair)
            {
                Debug.WriteLine($"Drop collection {coll.Name}");
                _connection.Database.DropCollectionAsync(coll.Name);
            }
        }

        IEnumerable<int> GetParentIds()
        {
            var colls = _database.Connection.ListCollectionsAsync().Result.ToListAsync().Result;
            var collsDraws = colls.Where(y => y.Values.Any(z => z.IsString && z.AsString.StartsWith("drawings_"))).ToList();
            foreach (var collsDraw in collsDraws)
            {
                var name = collsDraw.Values.FirstOrDefault(z => z.IsString && z.AsString.StartsWith("drawings_")).AsString;
                yield return Convert.ToInt32(name.Replace("drawings_", string.Empty));
            }
        }

        public void RebuildIndexes()
        {
            var colls = _database.Connection.ListCollectionsAsync().Result.ToListAsync().Result;
            var collsDraws = colls.Where(y => y.Values.Any(z => z.IsString && z.AsString.StartsWith("drawings_"))).ToList();
            foreach (var collsDraw in collsDraws)
            {
                var name = collsDraw.Values.FirstOrDefault(z => z.IsString && z.AsString.StartsWith("drawings_")).AsString;
                var collDraw = _database.Connection.GetCollection<Drawing>(name);
                var indexes = collDraw.Indexes.ListAsync().Result.ToListAsync().Result;
            }
        }

        public void RemoveHierarchicalWithoutParents(List<BadHierarchicalData> toRepair)
        {
            foreach (var grouppedTechRoutese in toRepair)
            {
                var ss = new SepDrawingDataManager(_database, grouppedTechRoutese.IdCollection);
                var list = ss.GetListCollection();
                var childrens = list.Where(x => x.ParentId == grouppedTechRoutese.IdRecord).ToList();
                foreach (var doc in childrens)
                {
                    ss.Delete(doc);
                }
            }
        }

        public List<GrouppedTechRoutes> CheckWorkHourDrawing()
        {
            var result = new List<GrouppedTechRoutes>();

            var dataManager = new MongoDbDataManager<WorkHourDrawing>(_database);
            var coll = dataManager.GetListCollection();
            return coll.GroupBy(x => new { x.DrawingId, x.TechOperationId })
                .Where(x => x.Count() > 1)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.Key.DrawingId)
                .Select(x => new GrouppedTechRoutes()
                {
                    Name = $"DrawingID: {x.Key.DrawingId}, " +
                                                               $"TechOPerationID: {x.Key.TechOperationId}",
                    Count = x.Count(),
                    Tag = new object[] { x.Key.DrawingId, x.Key.TechOperationId }
                }).OrderByDescending(x => x.Count).ToList();
        }

        public void RemoveDoublesWorkHourDrawing(List<GrouppedTechRoutes> toRepair)
        {
            var dataManager = new MongoDbDataManager<WorkHourDrawing>(_database);
            var coll = dataManager.GetListCollection();

            foreach (var grouppedTechRoutese in toRepair)
            {
                if (grouppedTechRoutese.Tag[0] != null && grouppedTechRoutese.Tag[1] != null)
                {
                    var workHours =
                        coll.Where(
                            x =>
                                x.DrawingId == (int)grouppedTechRoutese.Tag[0] &&
                                x.TechOperationId == (int)grouppedTechRoutese.Tag[1]).OrderByDescending(x => x.DrawingId).ToList();

                    foreach (var source in workHours.Skip(1))
                    {
                        dataManager.Delete(source);
                    }
                }
            }
        }
    }
}