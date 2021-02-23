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
            try
            {
                _collection.InsertOne(item);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _collection.Find(x => true).ToList();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public T GetById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault() ?? throw new ArgumentNullException();
        }

        public void Remove(string id)
        {
            try
            {
                _collection.DeleteOne(x => x.Id == id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public void Update(string id, T newItem)
        {
            try
            {
                _collection.ReplaceOne(x => x.Id == id, newItem);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
