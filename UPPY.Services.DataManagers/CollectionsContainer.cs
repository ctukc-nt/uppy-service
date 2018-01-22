using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            lock (CachedDocsCollection)
            {
                CachedDocsCollection.CachedPointTime = new DateTime(1970);
            }

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
            var collections = GetBsonDocumentsByType(t);

            lock (CachedIds)
            {
                var cachedColl = CachedIds.FirstOrDefault(x => x.Value.Object.Any(y => y == id));
                if (cachedColl.Value != null)
                {
                    var filtCollections = (from coll in collections
                                           let elemName = coll.Elements.FirstOrDefault(y => y.Name == "name")
                                           where elemName.Value.AsString == cachedColl.Key
                                           select coll).ToList();

                    return filtCollections[0];
                }
            }

            foreach (var document in collections)
            {
                var name = document.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString;
                var col = _mongoDb.GetCollection<BsonDocument>(name);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var count = col.FindAsync(filter).Result.ToListAsync().Result.Count;

                lock (CachedIds)
                {
                    if (!CachedIds.ContainsKey(name))
                    {
                        CreateCachedIdsAsync(col, name);
                    }
                }

                if (count == 1)
                {
                    lock (CachedIds)
                    {
                        CachedIds.Remove(name);
                    }

                    CreateCachedIdsAsync(col, name);

                    return document;
                }
            }

            return null;
        }

        private static Task CreateCachedIdsAsync(IMongoCollection<BsonDocument> col, string name)
        {
            var task = new Task(() =>
            {
                var projection = Builders<BsonDocument>.Projection.Include(new StringFieldDefinition<BsonDocument>("_id"));
                var cachedIdsCollection =
                    new CachedObject<List<int>>() { Object = null, CachedPointTime = DateTime.Now };
                var options = new FindOptions<BsonDocument, BsonDocument> { Projection = projection };
                var result = col.FindAsync(x => true, options).Result.ToListAsync().Result;
                var idsList = new List<int>();
                foreach (var bsd in result)
                {
                    var elem = bsd.Elements.FirstOrDefault(y => y.Name == "_id");
                    idsList.Add(elem.Value.AsInt32);
                }

                cachedIdsCollection.Object = idsList;

                lock (CachedIds)
                {
                    if (!CachedIds.ContainsKey(name))
                    {
                        CachedIds.Add(name, cachedIdsCollection);
                    }
                    else
                    {
                        if (CachedIds[name].CachedPointTime < cachedIdsCollection.CachedPointTime)
                        {
                            CachedIds[name] = cachedIdsCollection;
                        }
                    }
                }
            });

            task.Start();

            return task;
        }

        private static readonly Dictionary<string, CachedObject<List<int>>> CachedIds = new Dictionary<string, CachedObject<List<int>>>();
        private static readonly CachedObject<List<BsonDocument>> CachedDocsCollection = new CachedObject<List<BsonDocument>>() { CachedPointTime = new DateTime(1970) };

        private List<BsonDocument> GetCashedCollection()
        {
            lock (CachedDocsCollection)
            {
                if (DateTime.Now.Subtract(CachedDocsCollection.CachedPointTime).TotalSeconds > 60)
                {
                    CachedDocsCollection.CachedPointTime = DateTime.Now;
                    return CachedDocsCollection.Object = _mongoDb.ListCollectionsAsync().Result.ToListAsync().Result;
                }
                else
                {
                    return CachedDocsCollection.Object;
                }
            }
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


        public void InsertIdCollection(BsonDocument document, int id)
        {
            var collName = document.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString;
            lock (CachedIds)
            {
                if (CachedIds.ContainsKey(collName))
                {
                    if (CachedIds[collName].Object.All(x => x != id))
                        CachedIds[collName].Object.Add(id);
                }
            }
        }

        public void DeleteIdCollection(BsonDocument document, int id)
        {
            var collName = document.Elements.FirstOrDefault(y => y.Name == "name").Value.AsString;
            lock (CachedIds)
            {
                if (CachedIds.ContainsKey(collName))
                {
                    if (CachedIds[collName].Object.Any(x => x == id))
                        CachedIds[collName].Object.RemoveAll(x => x == id);
                }
            }
        }
    }
}