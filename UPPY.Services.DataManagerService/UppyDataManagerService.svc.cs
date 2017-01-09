using System.Collections.Generic;
using System.Linq;
using Core.Domain.Model;
using Core.Interfaces;
using Core.Security;
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
            var filterNullOrder = Builders<TaskToDistrict>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<TaskToDistrict>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection<TaskToDistrict>(orFilter);
        }

        public List<BillInnerShift> GetListBillInnerShiftByOrderId(int orderId)
        {
            _logger.Trace("Trace method BillInnerShift for document: {0}. Id: {1}", typeof(Drawing).Name, orderId);
            var filter = Builders<BillInnerShift>.Filter.Eq("OrderId", orderId);
            var filterNullOrder = Builders<BillInnerShift>.Filter.Eq("OrderId", (int?)null);
            var orFilter = Builders<BillInnerShift>.Filter.Or(filter, filterNullOrder);
            return _dataManagers.GetListCollection<BillInnerShift>(orFilter);
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

        public TechRoute UpdateTechRoute(TicketAutUser ticket, TechRoute doc)
        {
            _logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(TechRoute).Name, ticket, doc.Id);
            _dataManagers.Update(doc, ticket);
            return doc;
        }
    }
}