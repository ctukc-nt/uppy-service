using System.Collections.Generic;
using Core.Domain.Model;
using Core.Security;
using UPPY.Services.Core;
using System.Linq;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public partial class UppyDataManagerService : IUppyDataService
    {
			    #region BillInnerShift
	    
        public List<BillInnerShift> GetListBillInnerShift()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(BillInnerShift).Name);
		    return _dataManagers.GetListCollection<BillInnerShift>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<BillInnerShift>> GetHistoryListBillInnerShift()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<BillInnerShift>> GetHistoryDocBillInnerShift(BillInnerShift doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<BillInnerShift>> GetHistoryUpdatesBillInnerShift()
		{
			return null;
		}

        public BillInnerShift GetDocumentBillInnerShift(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(BillInnerShift).Name, id);
			return _dataManagers.GetDocument<BillInnerShift>(id);
		}
		
		public BillInnerShift InsertBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(BillInnerShift).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public BillInnerShift UpdateBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(BillInnerShift).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteBillInnerShift(TicketAutUser ticket, BillInnerShift doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(BillInnerShift).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region BillShift
	    
        public List<BillShift> GetListBillShift()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(BillShift).Name);
		    return _dataManagers.GetListCollection<BillShift>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<BillShift>> GetHistoryListBillShift()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<BillShift>> GetHistoryDocBillShift(BillShift doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<BillShift>> GetHistoryUpdatesBillShift()
		{
			return null;
		}

        public BillShift GetDocumentBillShift(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(BillShift).Name, id);
			return _dataManagers.GetDocument<BillShift>(id);
		}
		
		public BillShift InsertBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(BillShift).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public BillShift UpdateBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(BillShift).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteBillShift(TicketAutUser ticket, BillShift doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(BillShift).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region ExcludeDrawing
	    
        public List<ExcludeDrawing> GetListExcludeDrawing()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(ExcludeDrawing).Name);
		    return _dataManagers.GetListCollection<ExcludeDrawing>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<ExcludeDrawing>> GetHistoryListExcludeDrawing()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<ExcludeDrawing>> GetHistoryDocExcludeDrawing(ExcludeDrawing doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<ExcludeDrawing>> GetHistoryUpdatesExcludeDrawing()
		{
			return null;
		}

        public ExcludeDrawing GetDocumentExcludeDrawing(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(ExcludeDrawing).Name, id);
			return _dataManagers.GetDocument<ExcludeDrawing>(id);
		}
		
		public ExcludeDrawing InsertExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(ExcludeDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public ExcludeDrawing UpdateExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(ExcludeDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteExcludeDrawing(TicketAutUser ticket, ExcludeDrawing doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(ExcludeDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Gost
	    
        public List<Gost> GetListGost()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Gost).Name);
		    return _dataManagers.GetListCollection<Gost>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<Gost>> GetHistoryListGost()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<Gost>> GetHistoryDocGost(Gost doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<Gost>> GetHistoryUpdatesGost()
		{
			return null;
		}

        public Gost GetDocumentGost(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(Gost).Name, id);
			return _dataManagers.GetDocument<Gost>(id);
		}
		
		public Gost InsertGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(Gost).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Gost UpdateGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(Gost).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteGost(TicketAutUser ticket, Gost doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(Gost).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Order
	    
        public List<Order> GetListOrder()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Order).Name);
		    return _dataManagers.GetListCollection<Order>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<Order>> GetHistoryListOrder()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<Order>> GetHistoryDocOrder(Order doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<Order>> GetHistoryUpdatesOrder()
		{
			return null;
		}

        public Order GetDocumentOrder(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(Order).Name, id);
			return _dataManagers.GetDocument<Order>(id);
		}
		
		public Order InsertOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(Order).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Order UpdateOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(Order).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteOrder(TicketAutUser ticket, Order doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(Order).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region PackingList
	    
        public List<PackingList> GetListPackingList()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(PackingList).Name);
		    return _dataManagers.GetListCollection<PackingList>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<PackingList>> GetHistoryListPackingList()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<PackingList>> GetHistoryDocPackingList(PackingList doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<PackingList>> GetHistoryUpdatesPackingList()
		{
			return null;
		}

        public PackingList GetDocumentPackingList(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(PackingList).Name, id);
			return _dataManagers.GetDocument<PackingList>(id);
		}
		
		public PackingList InsertPackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(PackingList).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public PackingList UpdatePackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(PackingList).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeletePackingList(TicketAutUser ticket, PackingList doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(PackingList).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region ProfileWorkHour
	    
        public List<ProfileWorkHour> GetListProfileWorkHour()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(ProfileWorkHour).Name);
		    return _dataManagers.GetListCollection<ProfileWorkHour>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<ProfileWorkHour>> GetHistoryListProfileWorkHour()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<ProfileWorkHour>> GetHistoryDocProfileWorkHour(ProfileWorkHour doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<ProfileWorkHour>> GetHistoryUpdatesProfileWorkHour()
		{
			return null;
		}

        public ProfileWorkHour GetDocumentProfileWorkHour(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(ProfileWorkHour).Name, id);
			return _dataManagers.GetDocument<ProfileWorkHour>(id);
		}
		
		public ProfileWorkHour InsertProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(ProfileWorkHour).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public ProfileWorkHour UpdateProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(ProfileWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteProfileWorkHour(TicketAutUser ticket, ProfileWorkHour doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(ProfileWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region RateWorkHour
	    
        public List<RateWorkHour> GetListRateWorkHour()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(RateWorkHour).Name);
		    return _dataManagers.GetListCollection<RateWorkHour>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<RateWorkHour>> GetHistoryListRateWorkHour()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<RateWorkHour>> GetHistoryDocRateWorkHour(RateWorkHour doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<RateWorkHour>> GetHistoryUpdatesRateWorkHour()
		{
			return null;
		}

        public RateWorkHour GetDocumentRateWorkHour(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(RateWorkHour).Name, id);
			return _dataManagers.GetDocument<RateWorkHour>(id);
		}
		
		public RateWorkHour InsertRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(RateWorkHour).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public RateWorkHour UpdateRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(RateWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteRateWorkHour(TicketAutUser ticket, RateWorkHour doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(RateWorkHour).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Setting
	    
        public List<Setting> GetListSetting()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Setting).Name);
		    return _dataManagers.GetListCollection<Setting>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<Setting>> GetHistoryListSetting()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<Setting>> GetHistoryDocSetting(Setting doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<Setting>> GetHistoryUpdatesSetting()
		{
			return null;
		}

        public Setting GetDocumentSetting(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(Setting).Name, id);
			return _dataManagers.GetDocument<Setting>(id);
		}
		
		public Setting InsertSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(Setting).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Setting UpdateSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(Setting).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteSetting(TicketAutUser ticket, Setting doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(Setting).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region Standart
	    
        public List<Standart> GetListStandart()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Standart).Name);
		    return _dataManagers.GetListCollection<Standart>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<Standart>> GetHistoryListStandart()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<Standart>> GetHistoryDocStandart(Standart doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<Standart>> GetHistoryUpdatesStandart()
		{
			return null;
		}

        public Standart GetDocumentStandart(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(Standart).Name, id);
			return _dataManagers.GetDocument<Standart>(id);
		}
		
		public Standart InsertStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(Standart).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Standart UpdateStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(Standart).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteStandart(TicketAutUser ticket, Standart doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(Standart).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region StandartDrawing
	    
        public List<StandartDrawing> GetListStandartDrawing()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(StandartDrawing).Name);
		    return _dataManagers.GetListCollection<StandartDrawing>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<StandartDrawing>> GetHistoryListStandartDrawing()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<StandartDrawing>> GetHistoryDocStandartDrawing(StandartDrawing doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<StandartDrawing>> GetHistoryUpdatesStandartDrawing()
		{
			return null;
		}

        public StandartDrawing GetDocumentStandartDrawing(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(StandartDrawing).Name, id);
			return _dataManagers.GetDocument<StandartDrawing>(id);
		}
		
		public StandartDrawing InsertStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(StandartDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public StandartDrawing UpdateStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteStandartDrawing(TicketAutUser ticket, StandartDrawing doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(StandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region SuperTaskToDistrict
	    
        public List<SuperTaskToDistrict> GetListSuperTaskToDistrict()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(SuperTaskToDistrict).Name);
		    return _dataManagers.GetListCollection<SuperTaskToDistrict>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<SuperTaskToDistrict>> GetHistoryListSuperTaskToDistrict()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<SuperTaskToDistrict>> GetHistoryDocSuperTaskToDistrict(SuperTaskToDistrict doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<SuperTaskToDistrict>> GetHistoryUpdatesSuperTaskToDistrict()
		{
			return null;
		}

        public SuperTaskToDistrict GetDocumentSuperTaskToDistrict(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(SuperTaskToDistrict).Name, id);
			return _dataManagers.GetDocument<SuperTaskToDistrict>(id);
		}
		
		public SuperTaskToDistrict InsertSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(SuperTaskToDistrict).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public SuperTaskToDistrict UpdateSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(SuperTaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteSuperTaskToDistrict(TicketAutUser ticket, SuperTaskToDistrict doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(SuperTaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TaskToDistrict
	    
        public List<TaskToDistrict> GetListTaskToDistrict()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(TaskToDistrict).Name);
		    return _dataManagers.GetListCollection<TaskToDistrict>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<TaskToDistrict>> GetHistoryListTaskToDistrict()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<TaskToDistrict>> GetHistoryDocTaskToDistrict(TaskToDistrict doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<TaskToDistrict>> GetHistoryUpdatesTaskToDistrict()
		{
			return null;
		}

        public TaskToDistrict GetDocumentTaskToDistrict(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(TaskToDistrict).Name, id);
			return _dataManagers.GetDocument<TaskToDistrict>(id);
		}
		
		public TaskToDistrict InsertTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(TaskToDistrict).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TaskToDistrict UpdateTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(TaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTaskToDistrict(TicketAutUser ticket, TaskToDistrict doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(TaskToDistrict).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TechOperation
	    
        public List<TechOperation> GetListTechOperation()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(TechOperation).Name);
		    return _dataManagers.GetListCollection<TechOperation>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<TechOperation>> GetHistoryListTechOperation()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<TechOperation>> GetHistoryDocTechOperation(TechOperation doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<TechOperation>> GetHistoryUpdatesTechOperation()
		{
			return null;
		}

        public TechOperation GetDocumentTechOperation(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(TechOperation).Name, id);
			return _dataManagers.GetDocument<TechOperation>(id);
		}
		
		public TechOperation InsertTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(TechOperation).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TechOperation UpdateTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(TechOperation).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTechOperation(TicketAutUser ticket, TechOperation doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(TechOperation).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region TechRoute
	    
        public List<TechRoute> GetListTechRoute()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(TechRoute).Name);
		    return _dataManagers.GetListCollection<TechRoute>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<TechRoute>> GetHistoryListTechRoute()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<TechRoute>> GetHistoryDocTechRoute(TechRoute doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<TechRoute>> GetHistoryUpdatesTechRoute()
		{
			return null;
		}

        public TechRoute GetDocumentTechRoute(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(TechRoute).Name, id);
			return _dataManagers.GetDocument<TechRoute>(id);
		}
		
		public TechRoute InsertTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(TechRoute).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public TechRoute UpdateTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(TechRoute).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteTechRoute(TicketAutUser ticket, TechRoute doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(TechRoute).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region WorkHourStandartDrawing
	    
        public List<WorkHourStandartDrawing> GetListWorkHourStandartDrawing()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(WorkHourStandartDrawing).Name);
		    return _dataManagers.GetListCollection<WorkHourStandartDrawing>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<WorkHourStandartDrawing>> GetHistoryListWorkHourStandartDrawing()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<WorkHourStandartDrawing>> GetHistoryDocWorkHourStandartDrawing(WorkHourStandartDrawing doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<WorkHourStandartDrawing>> GetHistoryUpdatesWorkHourStandartDrawing()
		{
			return null;
		}

        public WorkHourStandartDrawing GetDocumentWorkHourStandartDrawing(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(WorkHourStandartDrawing).Name, id);
			return _dataManagers.GetDocument<WorkHourStandartDrawing>(id);
		}
		
		public WorkHourStandartDrawing InsertWorkHourStandartDrawing(TicketAutUser ticket, WorkHourStandartDrawing doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(WorkHourStandartDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public WorkHourStandartDrawing UpdateWorkHourStandartDrawing(TicketAutUser ticket, WorkHourStandartDrawing doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(WorkHourStandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteWorkHourStandartDrawing(TicketAutUser ticket, WorkHourStandartDrawing doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(WorkHourStandartDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    	    #region WorkHourDrawing
	    
        public List<WorkHourDrawing> GetListWorkHourDrawing()
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(WorkHourDrawing).Name);
		    return _dataManagers.GetListCollection<WorkHourDrawing>().OrderBy(x => x.Id).ToList();
		}

		[OperationContract]
		List<HistoryRecord<WorkHourDrawing>> GetHistoryListWorkHourDrawing()
		{
			return null;
		}


		[OperationContract]
        List<HistoryRecord<WorkHourDrawing>> GetHistoryDocWorkHourDrawing(WorkHourDrawing doc)
		{
			return null;
		}

		[OperationContract]
        List<HistoryRecord<WorkHourDrawing>> GetHistoryUpdatesWorkHourDrawing()
		{
			return null;
		}

        public WorkHourDrawing GetDocumentWorkHourDrawing(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(WorkHourDrawing).Name, id);
			return _dataManagers.GetDocument<WorkHourDrawing>(id);
		}
		
		public WorkHourDrawing InsertWorkHourDrawing(TicketAutUser ticket, WorkHourDrawing doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(WorkHourDrawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public WorkHourDrawing UpdateWorkHourDrawing(TicketAutUser ticket, WorkHourDrawing doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(WorkHourDrawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteWorkHourDrawing(TicketAutUser ticket, WorkHourDrawing doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(WorkHourDrawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}
	    
        #endregion
	    
	    
				#region Hierarchical Drawing

		public List<Drawing> GetContainsIdListDrawing(int id)
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Drawing).Name);
		    return _dataManagers.GetHierarchicalListCollection<Drawing>(id).OrderBy(x => x.Id).ToList();
		}

		public List<Drawing> GetChildrensForIdListDrawing(int id)
		{
			_logger.Trace("Trace method GetList for document: {0}", typeof(Drawing).Name);
		    return GetAllChildrensCashed(id, _dataManagers.GetHierarchicalListCollection<Drawing>(id).OrderBy(x => x.Id).ToList());
		}

        public Drawing GetDocumentDrawing(int id)
		{
			_logger.Trace("Trace method GetDocument for document: {0}. Id: {1}", typeof(Drawing).Name, id);
			return _dataManagers.GetDocument<Drawing>(id);
		}
		
		public Drawing InsertDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Insert for document: {0}. User: {1}", typeof(Drawing).Name, ticket);
		    _dataManagers.Insert(doc, ticket);
		    return doc;
		}
		
		public Drawing UpdateDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Update for document: {0}. Id: {2}. User: {1}", typeof(Drawing).Name, ticket, doc.Id);
		    _dataManagers.Update(doc, ticket);
			return doc;
		}
		
		public void DeleteDrawing(TicketAutUser ticket, Drawing doc)
		{
			_logger.Trace("Trace method Delete for document: {0}. Id: {2}. User: {1}", typeof(Drawing).Name, ticket, doc.Id);
		    _dataManagers.Delete(doc, ticket);
		}

		#endregion
		
	}
}