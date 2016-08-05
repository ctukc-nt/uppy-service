﻿using System;
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
    public class CommonEntityDataManagers
    {
        private static readonly object SectionGetId = new object();
        public CollectionsContainer CollectionsContainer { get; set; }

        public ObjectsAuditor Auditor { get; set; }

        public List<BsonDocument> GetListCollection(Type t)
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(t);
            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var res = coll.FindAsync(x => true).Result.ToListAsync().Result;

            return res;
        }

        public List<T> GetListCollection<T>()
        {
            var docColl = CollectionsContainer.GetBsonDocumentByType(typeof (T));
            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            return coll.FindAsync(x => true).Result.ToListAsync().Result;
        }

        public T GetDocument<T>(int id)
        {
            var docColl = CollectionsContainer.GetBsonDocumentContainsId(typeof (T), id);
            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            return BsonSerializer.Deserialize<T>(coll.FindAsync(filter).Result.ToListAsync().Result.FirstOrDefault());
        }

        public List<BsonDocument> GetHierarchicalListCollection(Type t, int id)
        {
            var docColl = CollectionsContainer.GetBsonDocumentContainsId(t, id);
            if (docColl != null)
            {
                var coll = CollectionsContainer.GetMongoCollection(docColl);
                return coll.FindAsync(x => true).Result.ToListAsync().Result;
            }

            return null;
        }

        public List<T> GetHierarchicalListCollection<T>(int id)
        {
            var docColl = CollectionsContainer.GetBsonDocumentContainsId(typeof(T), id);
            var coll = CollectionsContainer.GetMongoCollection<T>(docColl);

            return coll.FindAsync(x => true).Result.ToListAsync().Result;
        }

        public void Insert(IHierarchyEntity doc, ITicketAutUser user)
        {
            
        }

        public void Insert(IEntity doc, ITicketAutUser user)
        {
            if (doc == null)
                return;

            doc.Id = GetIdDocument(doc.GetType());

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType());
            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);

            coll.InsertOneAsync(bson).Wait();
            Auditor?.AuditOperation(OperationType.Insert, doc, user);
        }

        public void Update(IHierarchyEntity doc, ITicketAutUser user)
        {

        }

        public void Update(IEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType());
            var bson = doc.ToBsonDocument();
            bson.RemoveAt(0);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            coll.ReplaceOneAsync(filter, bson);

            Auditor?.AuditOperation(OperationType.Insert, doc, user);
        }

        public void Delete(IHierarchyEntity doc, ITicketAutUser user)
        {

        }

        public void Delete(IEntity doc, ITicketAutUser user)
        {
            if (doc?.Id == null)
                return;

            var docColl = CollectionsContainer.GetBsonDocumentByType(doc.GetType());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.Id);

            var coll = CollectionsContainer.GetMongoCollection(docColl);
            coll.DeleteOneAsync(filter);

            Auditor?.AuditOperation(OperationType.Delete, doc, user);
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
                    var res = coll.InsertOneAsync(new DocsId {DocId = 2, DocName = t.Name}.ToBsonDocument());
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