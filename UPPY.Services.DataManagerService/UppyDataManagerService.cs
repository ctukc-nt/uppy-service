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

        public BillInnerShift GetDocumentBillInnerShift(int id)
		{
			return _dataManagers.GetDocument<BillInnerShift>(id);
		}
		
		public BillInnerShift InsertBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(BillInnerShift).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public BillInnerShift UpdateBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(BillInnerShift).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(BillInnerShift).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region BillShift
	    
        public List<BillShift> GetListBillShift()
		{
		    return _dataManagers.GetListCollection<BillShift>();
		}

        public BillShift GetDocumentBillShift(int id)
		{
			return _dataManagers.GetDocument<BillShift>(id);
		}
		
		public BillShift InsertBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(BillShift).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public BillShift UpdateBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(BillShift).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(BillShift).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Drawing
	    
        public List<Drawing> GetListDrawing()
		{
		    return _dataManagers.GetListCollection<Drawing>();
		}

        public Drawing GetDocumentDrawing(int id)
		{
			return _dataManagers.GetDocument<Drawing>(id);
		}
		
		public Drawing InsertDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(Drawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Drawing UpdateDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(Drawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(Drawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region ExcludeDrawing
	    
        public List<ExcludeDrawing> GetListExcludeDrawing()
		{
		    return _dataManagers.GetListCollection<ExcludeDrawing>();
		}

        public ExcludeDrawing GetDocumentExcludeDrawing(int id)
		{
			return _dataManagers.GetDocument<ExcludeDrawing>(id);
		}
		
		public ExcludeDrawing InsertExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(ExcludeDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public ExcludeDrawing UpdateExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(ExcludeDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(ExcludeDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Gost
	    
        public List<Gost> GetListGost()
		{
		    return _dataManagers.GetListCollection<Gost>();
		}

        public Gost GetDocumentGost(int id)
		{
			return _dataManagers.GetDocument<Gost>(id);
		}
		
		public Gost InsertGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(Gost).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Gost UpdateGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(Gost).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(Gost).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Order
	    
        public List<Order> GetListOrder()
		{
		    return _dataManagers.GetListCollection<Order>();
		}

        public Order GetDocumentOrder(int id)
		{
			return _dataManagers.GetDocument<Order>(id);
		}
		
		public Order InsertOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(Order).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Order UpdateOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(Order).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(Order).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region PackingList
	    
        public List<PackingList> GetListPackingList()
		{
		    return _dataManagers.GetListCollection<PackingList>();
		}

        public PackingList GetDocumentPackingList(int id)
		{
			return _dataManagers.GetDocument<PackingList>(id);
		}
		
		public PackingList InsertPackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(PackingList).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public PackingList UpdatePackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(PackingList).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeletePackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(PackingList).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region ProfileWorkHour
	    
        public List<ProfileWorkHour> GetListProfileWorkHour()
		{
		    return _dataManagers.GetListCollection<ProfileWorkHour>();
		}

        public ProfileWorkHour GetDocumentProfileWorkHour(int id)
		{
			return _dataManagers.GetDocument<ProfileWorkHour>(id);
		}
		
		public ProfileWorkHour InsertProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(ProfileWorkHour).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public ProfileWorkHour UpdateProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(ProfileWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(ProfileWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region RateWorkHour
	    
        public List<RateWorkHour> GetListRateWorkHour()
		{
		    return _dataManagers.GetListCollection<RateWorkHour>();
		}

        public RateWorkHour GetDocumentRateWorkHour(int id)
		{
			return _dataManagers.GetDocument<RateWorkHour>(id);
		}
		
		public RateWorkHour InsertRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(RateWorkHour).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public RateWorkHour UpdateRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(RateWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(RateWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Setting
	    
        public List<Setting> GetListSetting()
		{
		    return _dataManagers.GetListCollection<Setting>();
		}

        public Setting GetDocumentSetting(int id)
		{
			return _dataManagers.GetDocument<Setting>(id);
		}
		
		public Setting InsertSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(Setting).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Setting UpdateSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(Setting).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(Setting).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Standart
	    
        public List<Standart> GetListStandart()
		{
		    return _dataManagers.GetListCollection<Standart>();
		}

        public Standart GetDocumentStandart(int id)
		{
			return _dataManagers.GetDocument<Standart>(id);
		}
		
		public Standart InsertStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(Standart).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Standart UpdateStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(Standart).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(Standart).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region StandartDrawing
	    
        public List<StandartDrawing> GetListStandartDrawing()
		{
		    return _dataManagers.GetListCollection<StandartDrawing>();
		}

        public StandartDrawing GetDocumentStandartDrawing(int id)
		{
			return _dataManagers.GetDocument<StandartDrawing>(id);
		}
		
		public StandartDrawing InsertStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(StandartDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public StandartDrawing UpdateStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region SuperTaskToDistrict
	    
        public List<SuperTaskToDistrict> GetListSuperTaskToDistrict()
		{
		    return _dataManagers.GetListCollection<SuperTaskToDistrict>();
		}

        public SuperTaskToDistrict GetDocumentSuperTaskToDistrict(int id)
		{
			return _dataManagers.GetDocument<SuperTaskToDistrict>(id);
		}
		
		public SuperTaskToDistrict InsertSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(SuperTaskToDistrict).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public SuperTaskToDistrict UpdateSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(SuperTaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(SuperTaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TaskToDistrict
	    
        public List<TaskToDistrict> GetListTaskToDistrict()
		{
		    return _dataManagers.GetListCollection<TaskToDistrict>();
		}

        public TaskToDistrict GetDocumentTaskToDistrict(int id)
		{
			return _dataManagers.GetDocument<TaskToDistrict>(id);
		}
		
		public TaskToDistrict InsertTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(TaskToDistrict).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TaskToDistrict UpdateTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(TaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(TaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TechOperation
	    
        public List<TechOperation> GetListTechOperation()
		{
		    return _dataManagers.GetListCollection<TechOperation>();
		}

        public TechOperation GetDocumentTechOperation(int id)
		{
			return _dataManagers.GetDocument<TechOperation>(id);
		}
		
		public TechOperation InsertTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(TechOperation).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TechOperation UpdateTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(TechOperation).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(TechOperation).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TechRoute
	    
        public List<TechRoute> GetListTechRoute()
		{
		    return _dataManagers.GetListCollection<TechRoute>();
		}

        public TechRoute GetDocumentTechRoute(int id)
		{
			return _dataManagers.GetDocument<TechRoute>(id);
		}
		
		public TechRoute InsertTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Insert for document {0}. User {1}", typeof(TechRoute).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TechRoute UpdateTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Update for document {0}. Id {2}. User {1}", typeof(TechRoute).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Delete for document {0}. Id {2}. User {1}", typeof(TechRoute).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    
	}
}