using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Comparers;
using Core.Domain.Interdaces;
using Core.Domain.Model;
using NLog;
using UPPY.Services.Core;
using UPPY.Services.Core.DataService;

namespace UPPY.ServerService
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select UppyService.svc or UppyService.svc.cs at the Solution Explorer and start debugging.
    public class UppyService : IUppyFilesService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUppyDataManagersFactory _dataManagersFactory;
        private static List<FileDrawingsOrders> _list = new List<FileDrawingsOrders>();
        private static bool _taskInProgress;
        private static bool _raiseRefreshList = true;
        private static bool _listInited;
        private static Timer _timerRefreshList = new Timer(state =>
        {
            if (!_taskInProgress)
            {
                _raiseRefreshList = true;
                _logger.Debug("Drop flag");
            }

        }, _listInited, (uint)120000, 10 * 60000);

        public UppyService(IUppyDataManagersFactory dataManagersFactory)
        {
            _logger.Trace("Create instance: {0}", "UppyService");
            _dataManagersFactory = dataManagersFactory;

            if (_raiseRefreshList)
            {
                var task = new Task(CreateAllFileDrawingsOrdersBw);
                task.Start();
            }
        }

        public List<FileDrawingsOrders> GetAllFileDrawingsOrders()
        {
            if (!_listInited && !_taskInProgress)
            {
                CreateAllFileDrawingsOrdersBw();
                return _list;
            }

            while (!_listInited)
            {
                Thread.Sleep(100);
            }

            return _list;
        }

        private void CreateAllFileDrawingsOrders()
        {
            if (_taskInProgress)
                return;

            _taskInProgress = true;
            _raiseRefreshList = false;

            try
            {
                _logger.Trace("Raise method: {0}", "GetAllFileDrawingsOrders");

                var orders = _dataManagersFactory.GetDataManager<Order>();
                var listOrders = orders.GetListCollection();

                var listTasks = new List<Task<List<FileDrawingsOrders>>>();

                foreach (var order in listOrders)
                {

                    var taskGetFiles = new Task<List<FileDrawingsOrders>>(() =>
                    {
                        _logger.Trace("Method: {0}, Begin Order: {1}", "GetAllFileDrawingsOrders", order.OrderNo);
                        var copyOrder = order;
                        var drawingsDataManager =
                            _dataManagersFactory.GetFilteredByTopParentDrawingClassDataManager(copyOrder.DrawingId);
                        var draws = drawingsDataManager.GetListCollection();

                        var res = (from drawing in draws
                                   from uppyFileInfo in drawing.Files
                                   select
                                       new FileDrawingsOrders
                                       {
                                           FileInfo = uppyFileInfo,
                                           Drawings = new List<Drawing> { drawing },
                                           Orders = new List<Order> { copyOrder }
                                       }).ToList();
                        _logger.Trace("Method: {0}, End Order: {1}", "GetAllFileDrawingsOrders", order.OrderNo);
                        return res;
                    });

                    taskGetFiles.Start();
                    listTasks.Add(taskGetFiles);
                }

                _logger.Trace("Method: {0}, Wait tasks.", "GetAllFileDrawingsOrders");

                Task.WhenAll(listTasks.Cast<Task>().ToArray());

                var resFiles = new List<FileDrawingsOrders>();
                foreach (var task in listTasks)
                {
                    resFiles.AddRange(task.Result);
                }

                _logger.Trace("Method: {0}, Group files.", "GetAllFileDrawingsOrders");

                var list =
                    resFiles.GroupBy(x => x.FileInfo, result => result, new UppyFileInfoComparer())
                        .Select(g => new FileDrawingsOrders
                        {
                            FileInfo = g.Key,
                            Drawings = g.Aggregate(
                                (res1, res2) =>
                                {
                                    res1.Drawings.AddRange(res2.Drawings);
                                    return res1;
                                }).Drawings.Distinct(new DrawingByIdComparer()).ToList(),
                            Orders = g.Aggregate((res1, res2) =>
                            {
                                res1.Orders.AddRange(res2.Orders);
                                return res1;
                            }).Orders.Distinct(new OrderEqualityComparer()).ToList()
                        });

                _logger.Info("End method: {0}", "GetAllFileDrawingsOrders");

                _list = list.ToList();
                _listInited = true;
                _taskInProgress = false;

            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            finally
            {
                _listInited = true;
                _taskInProgress = false;
            }
        }

        private void CreateAllFileDrawingsOrdersBw()
        {
            if (_taskInProgress)
                return;

            _taskInProgress = true;
            _raiseRefreshList = false;

            try
            {
                _logger.Trace("Raise method: {0}", "GetAllFileDrawingsOrders");
                _tempRes = new List<List<FileDrawingsOrders>>();
                var orders = _dataManagersFactory.GetDataManager<Order>();
                var listOrders = orders.GetListCollection();

                for (int i = 0; i < listOrders.Count; i++)
                {
                    _logger.Trace("Processed order: {0} from {1}.", i, listOrders.Count);
                    AddTask(listOrders[i]);
                }

                _logger.Trace("Method: {0}, Wait tasks.", "GetAllFileDrawingsOrders");

                while (true)
                {
                    if (_taskPool.Count>0)
                        Thread.Sleep(5000);
                    else
                    {
                        break;
                    }
                }


                var resFiles = new List<FileDrawingsOrders>();

                foreach (var task in _tempRes)
                {
                    resFiles.AddRange(task);
                }

                _logger.Trace("Method: {0}, Group files.", "GetAllFileDrawingsOrders");

                var list =
                    resFiles.GroupBy(x => x.FileInfo, result => result, new UppyFileInfoComparer())
                        .Select(g => new FileDrawingsOrders
                        {
                            FileInfo = g.Key,
                            Drawings = g.Aggregate(
                                (res1, res2) =>
                                {
                                    res1.Drawings.AddRange(res2.Drawings);
                                    return res1;
                                }).Drawings.Distinct(new DrawingByIdComparer()).ToList(),
                            Orders = g.Aggregate((res1, res2) =>
                            {
                                res1.Orders.AddRange(res2.Orders);
                                return res1;
                            }).Orders.Distinct(new OrderEqualityComparer()).ToList()
                        });

                _logger.Info("End method: {0}", "GetAllFileDrawingsOrders");

                _list = list.ToList();
                _listInited = true;
                _taskInProgress = false;

            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            finally
            {
                _listInited = true;
                _taskInProgress = false;
            }
        }

        public void ChangesInFiles()
        {
            _raiseRefreshList = false;
            _logger.Debug("Drop flag");
        }

        public List<FileDrawingsOrders> GetAllFileDrawingsOrdersByName(string designation)
        {
            Func<FileDrawingsOrders, bool> filter = null;
            if (false)
                filter = x => x.FileInfo.FileName.ToLowerInvariant().StartsWith(designation.ToLowerInvariant());
            else
            {
                filter = x => x.FileInfo.FileName.ToLowerInvariant().Contains(designation.ToLowerInvariant());
            }

            return CommonFind(filter);
        }

        private List<FileDrawingsOrders> CommonFind(Func<FileDrawingsOrders, bool> func)
        {
            return GetAllFileDrawingsOrders().Where(func).ToList();
        }

        private List<BackgroundWorker> _taskPool = new List<BackgroundWorker>();
        private List<List<FileDrawingsOrders>> _tempRes = new List<List<FileDrawingsOrders>>();
        private byte _maxTaskCount = 50;
        private bool _taskPoolFree;

        private void AddTask(Order order)
        {
            while (true)
            {
                if (_taskPool.Count <= _maxTaskCount)
                {
                    var bw = new BackgroundWorker();
                    bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                    bw.DoWork += (sender, args) =>
                    {
                        var res = new List<FileDrawingsOrders>();
                   
                        try
                        {
                            _logger.Trace("Method: {0}, Begin Order: {1}", "GetAllFileDrawingsOrders", order.OrderNo);
                            var copyOrder = order;
                            var drawingsDataManager =
                                _dataManagersFactory.GetFilteredByTopParentDrawingClassDataManager(copyOrder.DrawingId);
                            var draws = drawingsDataManager.GetListCollection();

                            res = (from drawing in draws
                                from uppyFileInfo in drawing.Files
                                select
                                new FileDrawingsOrders
                                {
                                    FileInfo = uppyFileInfo,
                                    Drawings = new List<Drawing> { drawing },
                                    Orders = new List<Order> { copyOrder }
                                }).ToList();
                            _logger.Trace("Method: {0}, End Order: {1}", "GetAllFileDrawingsOrders", order.OrderNo);
                            args.Result = res;
                        }
                        catch (Exception e)
                        {
                            _logger.Trace("ERROR! Method: {0}, Order: {1}. Exception: {2}", "AddTask", order.OrderNo, e);
                            args.Result = new List<FileDrawingsOrders>();
                        }
                    };

                    _taskPool.Add(bw);
                    bw.RunWorkerAsync();
                    break;
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _tempRes.Add((List<FileDrawingsOrders>)e.Result);
            _taskPool.Remove((BackgroundWorker) sender);
            ((BackgroundWorker)sender).Dispose();
            _taskPoolFree = true;
        }
    }
}