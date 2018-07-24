using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Security;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace UPPY.Services.DataManagers
{
    public class EntityCommonDataManagers
    {
        private static readonly object SectionGetId = new object();

        public CollectionsContainer CollectionsContainer { get; set; }

        public IObjectAuditor Auditor { get; set; }

        public List<T> GetListCollection<T>()
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(typeof(T));

            if (docColl == null)
                docColl = CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(typeof(T)));

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            return coll.FindAsync(x => true).Result.ToListAsync().Result;
        }

        public List<T> GetListCollection<T>(FilterDefinition<T> filter)
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(typeof(T));

            if (docColl == null)
                docColl = CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(typeof(T)));

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            return coll.FindAsync(filter).Result.ToListAsync().Result;
        }

        public T GetDocument<T>(int id)
        {
            var docColl = CollectionsContainer.GetBsonDocumentContainsId(typeof(T), id);

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            return BsonSerializer.Deserialize<T>(coll.FindAsync(filter).Result.ToListAsync().Result.FirstOrDefault());
        }

        public List<T> GetHierarchicalListCollection<T>(int id)
        {
            var docColl = CollectionsContainer.GetBsonDocumentContainsId(typeof(T), id);

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            return coll.FindAsync(x => true).Result.ToListAsync().Result;
        }

        public void Insert(IHierarchyEntity doc, ITicketAutUser user)
        {
            if (doc == null)
                return;

            doc.Id = GetIdDocument(doc.GetType());

            BsonDocument docColl;

            if (doc.ParentId.HasValue)
            {
                docColl = CollectionsContainer.GetBsonDocumentContainsId(doc.GetType(), doc.ParentId.Value);
                if (docColl == null)
                    throw new KeyNotFoundException();
            }
            else
            {
                docColl = CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(doc.GetType(), doc.Id.Value));
                if (docColl == null)
                    throw new KeyNotFoundException("Ошибка при создании коллекции");
            }

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.InsertOneAsync(bson).Wait();

            CollectionsContainer.InsertIdCollection(docColl, doc.Id.Value);

            Auditor?.AuditOperation(OperationType.Insert, doc, user);
        }

        public void RestoreDocument(IHierarchyEntity doc, ITicketAutUser user)
        {
            if (doc == null)
                return;

            BsonDocument docColl = null;

            if (doc.ParentId.HasValue)
            {
                docColl = CollectionsContainer.GetBsonDocumentContainsId(doc.GetType(), doc.ParentId.Value);
                if (docColl == null)
                    throw new KeyNotFoundException();
            }

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.InsertOneAsync(bson).Wait();

            CollectionsContainer.InsertIdCollection(docColl, doc.Id.Value);

            Auditor?.AuditOperation(OperationType.Insert, doc, user);
        }

        public void Insert(IEntity doc, ITicketAutUser user)
        {
            if (doc == null)
                return;

            doc.Id = GetIdDocument(doc.GetType());

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType()) ??
                          CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(doc.GetType()));

            if (docColl == null)
                throw new KeyNotFoundException("Ошибка при создании коллекции");

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);

            coll.InsertOneAsync(bson).Wait();
            Auditor?.AuditOperation(OperationType.Insert, doc, user);
        }

        public void Update(IHierarchyEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentContainsId(doc.GetType(), doc.Id.Value);

            if (docColl == null)
                throw new KeyNotFoundException();

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            coll.ReplaceOneAsync(filter, bson);

            Auditor?.AuditOperation(OperationType.Update, doc, user);
        }

        public void Update(IEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType());

            if (docColl == null)
                throw new KeyNotFoundException();

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            coll.ReplaceOneAsync(filter, bson);

            Auditor?.AuditOperation(OperationType.Update, doc, user);
        }

        public void Delete(IHierarchyEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentContainsId(doc.GetType(), doc.Id.Value);

            if (docColl == null)
                throw new KeyNotFoundException();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.DeleteOneAsync(filter);

            CollectionsContainer.DeleteIdCollection(docColl, doc.Id.Value);

            Auditor?.AuditOperation(OperationType.Delete, doc, user);
        }

        public void Delete(IEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType());

            if (docColl == null)
                throw new KeyNotFoundException();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.DeleteOneAsync(filter);

            Auditor?.AuditOperation(OperationType.Delete, doc, user);
        }

        public void Delete(Type docType, FilterDefinition<BsonDocument> filter)
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(docType);

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.DeleteOneAsync(filter);

            //Auditor?.AuditOperation(OperationType.Delete, doc, user);
        }

        private int GetIdDocument(Type t)
        {
            lock (SectionGetId)
            {
                var bsonDoc = CollectionsContainer.GetCollectionsByName("docsid").FirstOrDefault();
                var coll = CollectionsContainer.GetMongoCollection(bsonDoc);
                var filter = Builders<BsonDocument>.Filter.Eq("DocName", t.Name);


                var incrDocIdOptions = Builders<BsonDocument>.Update.Inc("DocId", 1);
                var rec = coll.Find(filter).CountAsync();
                rec.Wait();

                if (rec.Result == 0)
                {
                    var res = coll.InsertOneAsync(new DocsId { DocId = 2, DocName = t.Name }.ToBsonDocument());
                    res.Wait();

                    if (res.Exception != null)
                    {
                        return
                            BsonSerializer.Deserialize<DocsId>(
                                coll.FindOneAndUpdateAsync<BsonDocument>(filter, incrDocIdOptions)
                                    .Result).DocId;
                    }

                    return 1;
                }

                return
                    BsonSerializer.Deserialize<DocsId>(
                        coll.FindOneAndUpdateAsync<BsonDocument>(filter, incrDocIdOptions).Result).DocId;
            }
        }

        public List<T> GetDocuments<T>(List<int> ids)
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(typeof(T));

            if (docColl == null)
                docColl = CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(typeof(T)));

            if (docColl == null)
                throw new KeyNotFoundException();

            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            var filter = Builders<T>.Filter.In("Id", ids);
            return coll.FindAsync(filter).Result.ToListAsync().Result;
        }

        public void InsertBlockDocument(Object doc)
        {
            if (doc == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType()) ??
                          CollectionsContainer.CreateCollection(CollectionsContainer.GetNameCollection(doc.GetType()));

            if (docColl == null)
                throw new KeyNotFoundException("Ошибка при создании коллекции");

            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);

            coll.InsertOneAsync(bson).Wait();
        }


    }

    public enum OperationType
    {
        Insert,
        Update,
        Delete
    }

    internal class DocsId
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string DocName { get; set; }

        public int DocId { get; set; }
    }
}