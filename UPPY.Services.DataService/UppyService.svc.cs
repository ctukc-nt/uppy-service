using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Comparers;
using Core.Domain.Interdaces;
using Core.Domain.Model;
using NLog;
using UPPY.Logic.Classes;
using UPPY.Services.Core;

namespace UPPY.ServerService
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select UppyService.svc or UppyService.svc.cs at the Solution Explorer and start debugging.
    public class UppyService : IUppyFilesService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUppyDataManagersFactory _dataManagersFactory;

        public UppyService(IUppyDataManagersFactory dataManagersFactory)
        {
            _logger.Info("Create instance: {0}", "UppyService");
            _dataManagersFactory = dataManagersFactory;
        }

        public List<FileDrawingsOrders> GetAllFileDrawingsOrders()
        {
            _logger.Info("Raise method: {0}", "GetAllFileDrawingsOrders");

            var orders = _dataManagersFactory.GetDataManager<Order>();
            var listOrders = orders.GetListCollection();

            var listTasks = new List<Task<List<FileDrawingsOrders>>>();

            foreach (var order in listOrders.Take(2))
            {
                var taskGetFiles = new Task<List<FileDrawingsOrders>>(() =>
                {
                    var copyOrder = order;
                    var drawingsDataManager = _dataManagersFactory.GetFilteredByTopParentDrawingClassDataManager(copyOrder.DrawingId);
                    var draws = drawingsDataManager.GetListCollection();

                    var res = (from drawing in draws from uppyFileInfo in drawing.Files select new FileDrawingsOrders() { FileInfo = uppyFileInfo, Drawings = new List<Drawing>() { drawing }, Orders = new List<Order>() { copyOrder } }).ToList();
                    _logger.Info("Method: {0}, End Order: {1}", "GetAllFileDrawingsOrders", order.OrderNo);
                    return res;
                });

                taskGetFiles.Start();
                listTasks.Add(taskGetFiles);
            }

            Task.WhenAll(listTasks.Cast<Task>().ToArray());

            var resFiles = new List<FileDrawingsOrders>();
            foreach (var task in listTasks)
            {
                resFiles.AddRange(task.Result);
            }

            var sss = resFiles.GroupBy(x => x.FileInfo, result => result, new UppyFileInfoComparer()).Select(g => new FileDrawingsOrders()
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

            return sss.ToList();
        }

        public void ChangesInFiles()
        {
        }
    }
}