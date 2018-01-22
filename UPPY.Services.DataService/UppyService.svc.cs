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

        }, _listInited, (uint)1000 * 60 * 60, 1000 * 60 * 60);

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
            _logger.Trace("Method: {0}, List inited: {1}. Task in progress: {2}", "GetAllFileDrawingsOrders", _listInited, _taskInProgress);

            while (!_listInited)
            {
                Thread.Sleep(100);
            }

            _logger.Trace("End wait method: {0}, List inited: {1}. Task in progress: {2}", "GetAllFileDrawingsOrders", _listInited, _taskInProgress);

            return _list;
        }

        private void CreateAllFileDrawingsOrdersBw()
        {
            if (_taskInProgress)
                return;

            _taskInProgress = true;
            _raiseRefreshList = false;

            _taskPoolCount = 0;

            try
            {
                _logger.Trace("Raise method: {0}", "GetAllFileDrawingsOrders");
                _tempRes = new List<List<FileDrawingsOrders>>();
                var orders = _dataManagersFactory.GetDataManager<Order>();
                var listOrders = orders.GetListCollection().ToList();

                for (int i = 0; i < listOrders.Count; i++)
                {
                    _logger.Trace("Processed order: {0} from {1}.", i + 1, listOrders.Count);
                    AddTask(listOrders[i]);
                }

                _logger.Trace("Method: {0}, Wait tasks.", "GetAllFileDrawingsOrders");

                while (true)
                {
                    if (_taskPool.Count > 0 && _taskPoolCount > 0)
                    {
                        Thread.Sleep(5000);
                        _logger.Trace("Method: {0}, Task pool count: {1}", "GetAllFileDrawingsOrders", _taskPoolCount);
                    }
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

                _list = list.ToList();
                _listInited = true;
                _taskInProgress = false;
                _logger.Trace("End method: {0}, List inited: {1}. Task in progress: {2}", "GetAllFileDrawingsOrders", _listInited, _taskInProgress);

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
        private byte _taskPoolCount = 0;
        private List<List<FileDrawingsOrders>> _tempRes = new List<List<FileDrawingsOrders>>();
        private byte _maxTaskCount = 50;
        private bool _taskPoolFree;

        private void AddTask(Order order)
        {
            while (true)
            {
                if (_taskPoolCount <= _maxTaskCount)
                {
                    _taskPoolCount++;
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
                                _dataManagersFactory.GetFilteredDrawingsByContainsId(copyOrder.DrawingId);
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
            --_taskPoolCount;
            _tempRes.Add((List<FileDrawingsOrders>)e.Result);
            _taskPool.Remove((BackgroundWorker)sender);
            ((BackgroundWorker)sender).Dispose();
            _taskPoolFree = true;

        }
    }
}