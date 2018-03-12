using System.Collections.Generic;
using System.Linq;
using Core.Domain.Model;
using Core.Interfaces;
using Core.Security;
using Core.Versions;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [LoggingServiceBehavior]
    public partial class UppyDataManagerService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly EntityCommonDataManagers _dataManagers;
        private readonly HistoryEntityManager _historyManager;

        public UppyDataManagerService(EntityCommonDataManagers dataManagers, HistoryEntityManager historyManager)
        {
            _dataManagers = dataManagers;
            _historyManager = historyManager;
        }

        private List<T> GetAllChildrensCashed<T>(int? parentId, List<T> cashed) where T : IHierarchyEntity
        {
            var result = new List<T>();
            var childrens = GetChildrenDrawingsCashed(parentId, cashed);
            result.AddRange(childrens);
            foreach (var child in childrens)
            {
                result.AddRange(GetAllChildrensCashed(child.Id, cashed));
            }

            return result;
        }

        private List<T> GetChildrenDrawingsCashed<T>(int? parentId, List<T> cashed) where T : IHierarchyEntity
        {
            return cashed.Where(x => x.ParentId == parentId).ToList();
        }

        public List<TaskToDistrict> GetListTaskToDistrictByOrderId(int orderId)
        {
            _logger.Trace("Trace method GetListTaskToDistrictByOrderId for document: {0}. Id: {1}", typeof(Drawing).Name, orderId);
            var filter = Builders<TaskToDistrict>.Filter.Eq("OrderId", orderId);
            var filterArr = Builders<TaskToDistrict>.Filter.Where(y => y.Orders.Any(x => x.Id == orderId));
            var filterNullOrder = Builders<TaskToDistrict>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<TaskToDistrict>.Filter.Or(filter, filterNullOrder, filterArr);
            return _dataManagers.GetListCollection(orFilter);
        }

        public List<BillInnerShift> GetListBillInnerShiftByOrderId(int orderId)
        {
            _logger.Trace("Trace method BillInnerShift for document: {0}. Id: {1}", typeof(Drawing).Name, orderId);
            var filter = Builders<BillInnerShift>.Filter.Eq("OrderId", orderId);
            var filterNullOrder = Builders<BillInnerShift>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<BillInnerShift>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection<BillInnerShift>(orFilter);
        }

        public void CopyDrawingToAnother(TicketAutUser ticket, int sourceDrawingId, int parentId)
        {
            _logger.Trace("Trace method CopyDrawingToAnother for document: {0}. Source id: {2}. Dest id: {3} User: {1}", typeof(Drawing).Name, ticket, sourceDrawingId, parentId);
            var parent = _dataManagers.GetDocument<Drawing>(parentId);
            var copyDrawing = CopyDrawingToAnotherParent(sourceDrawingId, parent, ticket);

            if (copyDrawing != null)
            {
                var list = GetContainsIdListDrawing(sourceDrawingId);
                CopyChildrens(sourceDrawingId, copyDrawing, list, ticket);
            }
        }

        public List<PackingList> GetPackingListsByOrderId(int orderId)
        {
            _logger.Trace("Trace method GetPackingListsByOrderId for document: {0}. Id: {1}", typeof(Drawing).Name, orderId);
            var filter = Builders<PackingList>.Filter.Eq("OrderId", orderId);
            var filterNullOrder = Builders<PackingList>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<PackingList>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection<PackingList>(orFilter);
        }

        public List<WorkHourStandartDrawing> GetWorkHoursStandartsByStandartDrawingId(int standartDrawingId)
        {
            _logger.Trace("Trace method GetWorkHoursStandartsByStandartDrawingId for document: {0}. Id: {1}", typeof(WorkHourStandartDrawing).Name, standartDrawingId);
            var filter = Builders<WorkHourStandartDrawing>.Filter.Eq("StandartDrawingId", standartDrawingId);
            return _dataManagers.GetListCollection<WorkHourStandartDrawing>(filter);
        }

        private void CopyChildrens(int parentIdOld, Drawing parent, List<Drawing> list, ITicketAutUser ticket)
        {
            var childrensToCopy = list.Where(x => x.ParentId == parentIdOld);
            foreach (var drawing in childrensToCopy)
            {
                var newId = CopyDrawingToAnotherParent(drawing.Id.Value, parent, ticket);
                if (newId != null)
                {
                    CopyChildrens(drawing.Id.Value, newId, list, ticket);
                }
            }
        }

        private Drawing CopyDrawingToAnotherParent(int idSource, Drawing parent, ITicketAutUser ticket)
        {
            var copy = _dataManagers.GetDocument<Drawing>(idSource);

            if (copy != null)
            {
                copy.ParentId = parent?.Id;
                if (parent != null)
                    copy.CountAll = copy.Count * parent.CountAll;

                copy.RecalculateWeightAll();
                copy.Id = null;

                _dataManagers.Insert(copy, ticket);
                return copy;
            }

            return null;
        }


        #region StandartDrawing

        public List<StandartDrawing> GetListStandartDrawing()
        {
            _logger.Trace("Trace method GetList for document: {0}", typeof(StandartDrawing).Name);
            return _dataManagers.GetListCollection<StandartDrawing>().OrderBy(x => x.Id).ToList();
        }

        public List<HistoryRecord<StandartDrawing>> GetHistoryDocStandartDrawing(StandartDrawing doc)
        {
            _logger.Trace("Trace method GetHistoryList for document: {0}", typeof(StandartDrawing).Name);
            return _historyManager.GetHistoryDoc(doc);
        }

        public StandartDrawing GetDocumentStandartDrawing(int id)
        {
            _logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(StandartDrawing).Name, id);
            return _dataManagers.GetDocument<StandartDrawing>(id);
        }

        public StandartDrawing InsertStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
        {
            _logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(StandartDrawing).Name, ticket);
            _dataManagers.Insert((IEntity)doc, ticket);
            return doc;
        }

        public StandartDrawing UpdateStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
        {
            _logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
            _dataManagers.Update((IEntity)doc, ticket);
            return doc;
        }

        public void DeleteStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
        {
            _logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
            _dataManagers.Delete((IEntity)doc, ticket);
        }

        #endregion


        public WorkHourDrawing InsertWorkHourDrawing(TicketAutUser ticket, WorkHourDrawing doc)
        {
            _logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(WorkHourDrawing).Name, ticket);
            var filterDrawing = Builders<BsonDocument>.Filter.Eq("DrawingId", doc.DrawingId);
            var filtetTechOper = Builders<BsonDocument>.Filter.Eq("TechOperationId", doc.TechOperationId);
            var filter = Builders<BsonDocument>.Filter.And(filterDrawing, filtetTechOper);
            _dataManagers.Delete(doc.GetType(), filter);
            if (doc.WorkHour > 0)
            {
                _dataManagers.Insert(doc, ticket);
            }
            else
            {
                doc.Id = null;
            }

            return doc;
        }

        public WorkHourStandartDrawing InsertWorkHourStandartDrawing(TicketAutUser ticket, WorkHourStandartDrawing doc)
        {
            _logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(WorkHourStandartDrawing).Name, ticket);
            var filterDrawing = Builders<BsonDocument>.Filter.Eq("StandartDrawingId", doc.StandartDrawingId);
            var filtetTechOper = Builders<BsonDocument>.Filter.Eq("TechOperationId", doc.TechOperationId);
            var filter = Builders<BsonDocument>.Filter.And(filterDrawing, filtetTechOper);

            _dataManagers.Delete(doc.GetType(), filter);
            if (doc.WorkHour > 0)
            {
                _dataManagers.Insert(doc, ticket);
            }
            else
            {
                doc.Id = null;
            }
            return doc;
        }

        public TechRoute InsertTechRoute(TicketAutUser ticket, TechRoute doc)
        {
            _logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(TechRoute).Name, ticket);
            var filter = Builders<TechRoute>.Filter.Eq("Name", doc.Name);
            var coll = _dataManagers.GetListCollection(filter);
            if (coll.Count > 0)
            {
                return coll.FirstOrDefault();
            }

            _dataManagers.Insert(doc, ticket);
            return doc;
        }


        public List<GangTaskToDistrict> GetListGangTaskToDistrictByTaskId(int taskId)
        {
            _logger.Trace("Trace method GetListGangTaskToDistrictByTaskId for document: {0}. TaskId: {1}", typeof(GangTaskToDistrict).Name, taskId);
            var filter = Builders<GangTaskToDistrict>.Filter.Eq("TaskToDistrictId", taskId);
            //var filterNullOrder = Builders<GangTaskToDistrict>.Filter.Eq("OrderId", (int?)null);
            //var orFilter = Builders<GangTaskToDistrict>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection(filter);
        }

        public List<GangTaskToDistrict> GetListGangTaskToDistrictByOrderId(int orderId)
        {
            _logger.Trace("Trace method GetListGangTaskToDistrictByOrderId for document: {0}. OrderId: {1}", typeof(GangTaskToDistrict).Name, orderId);
            var filter = Builders<GangTaskToDistrict>.Filter.Eq("OrderId", orderId);
            var filterNullOrder = Builders<GangTaskToDistrict>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<GangTaskToDistrict>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection(orFilter);
        }

        public WorkHourDrawing GetWorkHourDrawingDocument(int techOperationId, int drawingId)
        {
            _logger.Trace("Trace method Insert for document: {0}. ", typeof(WorkHourDrawing).Name);
            var filterDrawing = Builders<WorkHourDrawing>.Filter.Eq("DrawingId", drawingId);
            var filtetTechOper = Builders<WorkHourDrawing>.Filter.Eq("TechOperationId", techOperationId);
            var orFilter = Builders<WorkHourDrawing>.Filter.And(filterDrawing, filtetTechOper);
            var coll = _dataManagers.GetListCollection(orFilter);
            if (coll.Count > 0)
            {
                return coll.FirstOrDefault();
            }

            return null;
        }

        public List<SuperStandart> GetLazyListSuperStandart(TicketAutUser ticket)
        {
            _logger.Trace("Trace method GetLazyList for document: {0}", typeof(SuperStandart).Name);
            var res = _dataManagers.GetListCollection<SuperStandart>();
            foreach (var superStandart in res)
            {
                foreach (var superStandartListStandart in superStandart.ListStandarts)
                {
                    superStandartListStandart.Positions = new List<PositionStandart>();
                }
            }

            return res.OrderBy(x => x.Id).ToList();
        }

        public List<Standart> GetLazyListStandart(TicketAutUser ticket)
        {
            _logger.Trace("Trace method GetLazyList for document: {0}", typeof(Standart).Name);
            var res = _dataManagers.GetListCollection<Standart>();
            foreach (var standart in res)
            {
                standart.Positions = new List<PositionStandart>();
            }

            return res.OrderBy(x => x.Id).ToList();
        }

        public List<StandartDrawing> GetDocumentsStandartDrawing(List<int> ids)
        {
            _logger.Trace("Trace method GetDocuments for document: {0}", typeof(StandartDrawing).Name);
            return _dataManagers.GetDocuments<StandartDrawing>(ids);
        }
    }
}