using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UPPY.Services.DataManagers
{
    public class CollectionsContainer
    {
        private readonly IMongoDatabase _mongoDb;

        public CollectionsContainer(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public BsonDocument CreateCollection(string name)
        {
            _mongoDb.CreateCollectionAsync(name).Wait();
            return GetCollectionsByName(name).FirstOrDefault();
        }

        public BsonDocument GetBsonDocumentByType(Type t)
        {
            var collections = GetBsonDocumentsByType(t);
            return collections.FirstOrDefault();
        }

        public IMongoCollection<BsonDocument> GetMongoCollection(BsonDocument typeCollection)
        {
            return _mongoDb.GetCollection<BsonDocument>(typeCollection.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString);
        }

        public IMongoCollection<T> GetMongoCollection<T>(BsonDocument typeCollection)
        {
            return _mongoDb.GetCollection<T>(typeCollection.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString);
        }

        public List<BsonDocument> GetCollectionsByName(string name)
        {
            var collections = GetCashedCollection();

            var filtCollections = (from coll in collections
                                   let elemName = coll.Elements.FirstOrDefault(y => y.Name == "name")
                                   where elemName.Value.AsString == name
                                   select coll).ToList();
            return filtCollections;
        }

        public BsonDocument GetBsonDocumentContainsId(Type t, int id)
        {
            var coll = GetBsonDocumentsByType(t);

            foreach (var document in coll)
            {
                var name = document.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString;
                var col = _mongoDb.GetCollection<BsonDocument>(name);

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                var count = col.FindAsync(filter).Result.ToListAsync().Result.Count;

                if (count == 1)
                    return document;
            }

            return null;
        }

        private List<BsonDocument> GetCashedCollection()
        {
            return _mongoDb.ListCollectionsAsync().Result.ToListAsync().Result;
        }

        private List<BsonDocument> GetBsonDocumentsByType(Type t)
        {
            var collections = GetCashedCollection();

            var filtCollections = (from coll in collections
                                   let elemName = coll.Elements.FirstOrDefault(y => y.Name == "name")
                                   where elemName.Value.AsString == GetNameCollection(t) || (elemName.Value.AsString.Contains("_") && elemName.Value.AsString.StartsWith(GetNameCollection(t)))
                                   select coll).ToList();

            return filtCollections;
        }

        public static string GetNameCollection(Type t)
        {
            return t.Name.ToLowerInvariant() + "s";
        }

        public static string GetNameCollection(Type t, int id)
        {
            return t.Name.ToLowerInvariant() + "s" + "_" + id;
        }


    }
}