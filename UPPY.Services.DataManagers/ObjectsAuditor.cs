using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Security;
using Mongo.Common;
using Mongo.Common.MongoAdditional.Service;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UPPY.Services.DataManagers
{
    public class ObjectsAuditor
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
                var collection = _mongoDb.GetCollection<Audit>("audits_" + operationType);
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
    }

    public enum AuditResult
    {
        Success,

        NonAudited
    }
}