using System;
using System.Collections.Generic;
using Core.Domain.Model;
using Core.Security;
using UPPY.Services.Core;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public partial class UppyDataManagerService : IUppyDataService
    {
        #region BillInnerShift

        public List<BillInnerShift> GetListBillInnerShift()
        {
            return _dataManagers.GetListCollection<BillInnerShift>();
        }

        public BillInnerShift InsertBillInnerShift(ITicketAutUser ticket, BillInnerShift doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateBillInnerShift(ITicketAutUser ticket, BillInnerShift doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteBillInnerShift(ITicketAutUser ticket, BillInnerShift doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region BillShift

        public List<BillShift> GetListBillShift()
        {
            return _dataManagers.GetListCollection<BillShift>();
        }

        public BillShift InsertBillShift(ITicketAutUser ticket, BillShift doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateBillShift(ITicketAutUser ticket, BillShift doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteBillShift(ITicketAutUser ticket, BillShift doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region Drawing

        public List<Drawing> GetListDrawing()
        {
            return _dataManagers.GetListCollection<Drawing>();
        }

        public Drawing InsertDrawing(ITicketAutUser ticket, Drawing doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateDrawing(ITicketAutUser ticket, Drawing doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteDrawing(ITicketAutUser ticket, Drawing doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region ExcludeDrawing

        public List<ExcludeDrawing> GetListExcludeDrawing()
        {
            return _dataManagers.GetListCollection<ExcludeDrawing>();
        }

        public ExcludeDrawing InsertExcludeDrawing(ITicketAutUser ticket, ExcludeDrawing doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateExcludeDrawing(ITicketAutUser ticket, ExcludeDrawing doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteExcludeDrawing(ITicketAutUser ticket, ExcludeDrawing doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region Gost

        public List<Gost> GetListGost()
        {
            return _dataManagers.GetListCollection<Gost>();
        }

        public Gost InsertGost(ITicketAutUser ticket, Gost doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateGost(ITicketAutUser ticket, Gost doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteGost(ITicketAutUser ticket, Gost doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region Order

        public List<Order> GetListOrder()
        {
            return _dataManagers.GetListCollection<Order>();
        }

        public Order InsertOrder(ITicketAutUser ticket, Order doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateOrder(ITicketAutUser ticket, Order doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteOrder(ITicketAutUser ticket, Order doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region PackingList

        public List<PackingList> GetListPackingList()
        {
            return _dataManagers.GetListCollection<PackingList>();
        }

        public PackingList InsertPackingList(ITicketAutUser ticket, PackingList doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdatePackingList(ITicketAutUser ticket, PackingList doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeletePackingList(ITicketAutUser ticket, PackingList doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region ProfileWorkHour

        public List<ProfileWorkHour> GetListProfileWorkHour()
        {
            return _dataManagers.GetListCollection<ProfileWorkHour>();
        }

        public ProfileWorkHour InsertProfileWorkHour(ITicketAutUser ticket, ProfileWorkHour doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateProfileWorkHour(ITicketAutUser ticket, ProfileWorkHour doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteProfileWorkHour(ITicketAutUser ticket, ProfileWorkHour doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region RateWorkHour

        public List<RateWorkHour> GetListRateWorkHour()
        {
            return _dataManagers.GetListCollection<RateWorkHour>();
        }

        public RateWorkHour InsertRateWorkHour(ITicketAutUser ticket, RateWorkHour doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateRateWorkHour(ITicketAutUser ticket, RateWorkHour doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteRateWorkHour(ITicketAutUser ticket, RateWorkHour doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region Setting

        public List<Setting> GetListSetting()
        {
            return _dataManagers.GetListCollection<Setting>();
        }

        public Setting InsertSetting(ITicketAutUser ticket, Setting doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateSetting(ITicketAutUser ticket, Setting doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteSetting(ITicketAutUser ticket, Setting doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region Standart

        public List<Standart> GetListStandart()
        {
            return _dataManagers.GetListCollection<Standart>();
        }

        public Standart InsertStandart(ITicketAutUser ticket, Standart doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateStandart(ITicketAutUser ticket, Standart doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteStandart(ITicketAutUser ticket, Standart doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region StandartDrawing

        public List<StandartDrawing> GetListStandartDrawing()
        {
            return _dataManagers.GetListCollection<StandartDrawing>();
        }

        public StandartDrawing InsertStandartDrawing(ITicketAutUser ticket, StandartDrawing doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateStandartDrawing(ITicketAutUser ticket, StandartDrawing doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteStandartDrawing(ITicketAutUser ticket, StandartDrawing doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region SuperTaskToDistrict

        public List<SuperTaskToDistrict> GetListSuperTaskToDistrict()
        {
            return _dataManagers.GetListCollection<SuperTaskToDistrict>();
        }

        public SuperTaskToDistrict InsertSuperTaskToDistrict(ITicketAutUser ticket, SuperTaskToDistrict doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateSuperTaskToDistrict(ITicketAutUser ticket, SuperTaskToDistrict doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteSuperTaskToDistrict(ITicketAutUser ticket, SuperTaskToDistrict doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region TaskToDistrict

        public List<TaskToDistrict> GetListTaskToDistrict()
        {
            return _dataManagers.GetListCollection<TaskToDistrict>();
        }

        public TaskToDistrict InsertTaskToDistrict(ITicketAutUser ticket, TaskToDistrict doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateTaskToDistrict(ITicketAutUser ticket, TaskToDistrict doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteTaskToDistrict(ITicketAutUser ticket, TaskToDistrict doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region TechOperation

        public List<TechOperation> GetListTechOperation()
        {
            return _dataManagers.GetListCollection<TechOperation>();
        }

        public TechOperation InsertTechOperation(ITicketAutUser ticket, TechOperation doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateTechOperation(ITicketAutUser ticket, TechOperation doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteTechOperation(ITicketAutUser ticket, TechOperation doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        #endregion

        #region TechRoute

        public List<TechRoute> GetListTechRoute()
        {
            return _dataManagers.GetListCollection<TechRoute>();
        }

        public TechRoute InsertTechRoute(ITicketAutUser ticket, TechRoute doc)
        {
            _dataManagers.Insert(doc, ticket);
            return doc;
        }

        public void UpdateTechRoute(ITicketAutUser ticket, TechRoute doc)
        {
            _dataManagers.Update(doc, ticket);
        }

        public void DeleteTechRoute(ITicketAutUser ticket, TechRoute doc)
        {
            _dataManagers.Delete(doc, ticket);
        }

        BillInnerShift IUppyDataService.InsertBillShift(ITicketAutUser ticket, BillShift doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertDrawing(ITicketAutUser ticket, Drawing doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertExcludeDrawing(ITicketAutUser ticket, ExcludeDrawing doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertGost(ITicketAutUser ticket, Gost doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertOrder(ITicketAutUser ticket, Order doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertPackingList(ITicketAutUser ticket, PackingList doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertProfileWorkHour(ITicketAutUser ticket, ProfileWorkHour doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertRateWorkHour(ITicketAutUser ticket, RateWorkHour doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertSetting(ITicketAutUser ticket, Setting doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertStandart(ITicketAutUser ticket, Standart doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertStandartDrawing(ITicketAutUser ticket, StandartDrawing doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertSuperTaskToDistrict(ITicketAutUser ticket, SuperTaskToDistrict doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertTaskToDistrict(ITicketAutUser ticket, TaskToDistrict doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertTechOperation(ITicketAutUser ticket, TechOperation doc)
        {
            throw new NotImplementedException();
        }

        BillInnerShift IUppyDataService.InsertTechRoute(ITicketAutUser ticket, TechRoute doc)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}