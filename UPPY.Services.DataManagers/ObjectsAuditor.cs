using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Security;
using Mongo.Common;
using Mongo.Common.MongoAdditional.Service;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UPPY.Services.DataManagers
{
    public class ObjectsAuditor : IGetterHistoryRecords, IObjectAuditor
    {
        private readonly IMongoDatabase _mongoDb;

        public ObjectsAuditor(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public Task<AuditResult> AuditOperation(OperationType operationType, IEntity doc, ITicketAutUser user)
        {
            var task = new Task<AuditResult>(() =>
            {
                var docJson = doc.ToJson();

                var collection = _mongoDb.GetCollection<Audit>(GetCollAuditName(operationType));
                collection.InsertOneAsync(new Audit()
                {
                    DateOperation = DateTime.Now,
                    JsonFormatObject = docJson,
                    ObjectType = doc.GetType().Name,
                    Operation = operationType.ToString(),
                    Login = user.Login
                });

                return AuditResult.Success;
            });

            task.Start();

            return task;
        }

        public List<Audit> GetDocOperation<T>(OperationType operation, T doc) where T : IEntity
        {
            var filterByOperation = Builders<Audit>.Filter.Eq("Operation", operation);
            var filterByType = Builders<Audit>.Filter.Eq("ObjectType", typeof(T).Name);
            var bsonRegexId = new BsonRegularExpression("(\"_id\" : " + doc.Id + ")");
            var filterById = Builders<Audit>.Filter.Regex("JsonFormatObject", bsonRegexId);

            var commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType, filterById);
            var collName = GetCollAuditName(operation);

            return ListOperations<T>(collName, commonFilter).Result;
        }

        public List<Audit> GetDeletedByParentId<T>(int parentId) where T : IEntity
        {
            var filterByOperation = Builders<Audit>.Filter.Eq("Operation", "Delete");
            var filterByType = Builders<Audit>.Filter.Eq("ObjectType", typeof(T).Name);
            var bsonRegexId = new BsonRegularExpression("(\"ParentId\" : " + parentId + ")");
            var filterById = Builders<Audit>.Filter.Regex("JsonFormatObject", bsonRegexId);
            var collName = GetCollAuditName(OperationType.Delete);
            var commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType, filterById);
            return ListOperations<T>(collName, commonFilter).Result;
        }

        public List<Audit> GetDocOperations<T>(T doc) where T : IEntity
        {
            var filterByType = Builders<Audit>.Filter.Eq("ObjectType", typeof(T).Name);
            var bsonRegexId = new BsonRegularExpression("/(\"_id\" : " + doc.Id+",)/");
            var filterById = Builders<Audit>.Filter.Regex("JsonFormatObject", bsonRegexId);

            var filterByOperation = Builders<Audit>.Filter.Eq("Operation", OperationType.Insert.ToString());
            var commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType, filterById);
            var collName = GetCollAuditName(OperationType.Insert);

            var insertOper = ListOperations<T>(collName, commonFilter);

            filterByOperation = Builders<Audit>.Filter.Eq("Operation", OperationType.Update.ToString());
            commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType, filterById);
            collName = GetCollAuditName(OperationType.Update);

            var updateOper = ListOperations<T>(collName, commonFilter);

            filterByOperation = Builders<Audit>.Filter.Eq("Operation", OperationType.Delete.ToString());
            commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType, filterById);
            collName = GetCollAuditName(OperationType.Delete);

            var deleteOper = ListOperations<T>(collName, commonFilter);
            Task.WaitAll(insertOper, updateOper, deleteOper);
            var res = insertOper.Result.Union(updateOper.Result).Union(deleteOper.Result).OrderBy(x => x.DateOperation).ToList();

            return res;
        }

        public List<Audit> GetListOperations<T>(OperationType operation)
        {
            var filterByOperation = Builders<Audit>.Filter.Eq("Operation", operation.ToString());
            var filterByType = Builders<Audit>.Filter.Eq("ObjectType", typeof(T).Name);
            var commonFilter = Builders<Audit>.Filter.And(filterByOperation, filterByType);
            var collName = GetCollAuditName(operation);

            return ListOperations<T>(collName, commonFilter).Result;
        }

        public List<Audit> GetListAllOperations<T>()
        {
            var collName = GetCollAuditName(OperationType.Insert);
            var filterByType = Builders<Audit>.Filter.Eq("ObjectType", typeof(T).Name);
            var insertOper = ListOperations<T>(collName, filterByType);

            collName = GetCollAuditName(OperationType.Update);
            var updateOper = ListOperations<T>(collName, filterByType);

            collName = GetCollAuditName(OperationType.Delete);
            var deleteOper = ListOperations<T>(collName, filterByType);

            Task.WaitAll(insertOper, updateOper, deleteOper);
            var res = insertOper.Result.Union(updateOper.Result).Union(deleteOper.Result).OrderBy(x => x.DateOperation).ToList();

            return res;
        }

        private string GetCollAuditName(OperationType oper)
        {
            return "audits_" + oper;
        }

        private Task<List<Audit>> ListOperations<T>(string collName, FilterDefinition<Audit> commonFilter)
        {
            var collection = _mongoDb.GetCollection<Audit>(collName);
            return collection.FindAsync(commonFilter).Result.ToListAsync();
        }
    }

    public interface IObjectAuditor
    {
        Task<AuditResult> AuditOperation(OperationType operationType, IEntity doc, ITicketAutUser user);
    }

    public interface IGetterHistoryRecords
    {
        List<Audit> GetDocOperations<T>(T doc) where T : IEntity;
    }

    public enum AuditResult
    {
        Success,

        NonAudited
    }
}