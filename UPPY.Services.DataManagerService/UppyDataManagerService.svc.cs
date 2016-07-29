using System;
using System.Collections.Generic;
using Core.Domain.Model;
using Core.Security;
using NLog;
using UPPY.Services.Core;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UppyDataManagerService : IUppyDataService
    {

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly CommonEntityDataManagers _dataManagers;

        public UppyDataManagerService(CommonEntityDataManagers dataManagers)
        {
            _dataManagers = dataManagers;
            _logger.Trace("Created instance");
        }

        public List<BillInnerShift> GetListBillInnerShift()
        {
            return _dataManagers.GetListCollection<BillInnerShift>();
        }

        public BillInnerShift InsertBillInnerShift(BillInnerShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateBillInnerShift(BillInnerShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteBillInnerShift(BillInnerShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Delete(doc, ticket);
        }

        public List<BillShift> GetListBillShift()
        {
            return _dataManagers.GetListCollection<BillShift>();
        }

        public BillShift InsertBillShift(BillShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateBillShift(BillShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteBillShift(BillShift doc, ITicketAutUser ticket)
        {
            _dataManagers.Delete(doc, ticket);
        }

        public List<Drawing> GetListDrawing()
        {
            throw new NotImplementedException();
        }

        public Drawing InsertDrawing(Drawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrawing(Drawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteDrawing(Drawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public List<ExcludeDrawing> GetListExcludeDrawing()
        {
            throw new NotImplementedException();
        }

        public Drawing InsertExcludeDrawing(ExcludeDrawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void UpdateExcludeDrawing(ExcludeDrawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteExcludeDrawing(ExcludeDrawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public List<Gost> GetListGost()
        {
            throw new NotImplementedException();
        }

        public Drawing InsertGost(Gost doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void UpdateGost(Gost doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteGost(ExcludeDrawing doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public List<TechOperation> GetListTechOperation()
        {
            throw new NotImplementedException();
        }

        public TechOperation InsertTechOperation(TechOperation doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void UpdateTechOperation(TechOperation doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteTechOperation(TechOperation doc, ITicketAutUser ticket)
        {
            throw new NotImplementedException();
        }
    }
}
