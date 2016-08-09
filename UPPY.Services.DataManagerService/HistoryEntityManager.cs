using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Versions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    public class HistoryEntityManager
    {
        public IGetterHistoryRecords Auditor { get; set; }

        public List<HistoryRecord<T>> GetHistoryList<T>() where T : IEntity
        {
            return null;
        }

        public List<HistoryRecord<T>> GetHistoryListByOperation<T>(OperationType oper) where T : IEntity
        {
            return null;
        }

        public List<HistoryRecord<T>> GetHistoryDoc<T>(T doc) where T : IEntity
        {
            return Auditor?.GetDocOperations(doc).Select(x => new HistoryRecord<T>() { DateTime = x.DateOperation, Operation = x.Operation, Operator = x.Login, Object = (T)BsonSerializer.Deserialize(BsonDocument.Parse(x.JsonFormatObject), typeof(T)) }).ToList();
        }
    }
}