using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected IMongoCollection<T> _collection;

        public Repository(string connectionString, string collection)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoUrl);

            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _collection = database.GetCollection<T>(collection);
        }

        public void Create(T item)
        {
            _collection.InsertOne(item);
        }

        public List<T> GetAll()
        {
            return _collection.Find(x => true).ToList();
        }

        public T GetById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public void Update(string id, T newItem)
        {
            _collection.ReplaceOne(x => x.Id == id, newItem);
        }
    }
}
