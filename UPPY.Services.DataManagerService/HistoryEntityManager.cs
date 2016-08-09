using System.Collections.Generic;
using Core.Interfaces;
using Core.Versions;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    public class HistoryEntityManager
    {
        public ObjectsAuditor Auditor { get; set; }

        public List<HistoryRecord<T>> GetHistoryList<T>() where T:IEntity
        {
            return null;
        }

        public List<HistoryRecord<T>> GetHistoryListByOperation<T>(OperationType oper) where T:IEntity
		{
			return null;
		}

        public List<HistoryRecord<T>> GetHistoryDoc<T>(T doc) where T:IEntity
        {
            return null;
        }
    }
}