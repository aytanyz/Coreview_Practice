using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WEBApi.Models;

namespace WEBApi.Services
{
    public class OrderService : IServiceRepository<Order>
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MyDB");

            _orders = database.GetCollection<Order>("Orders");
        }

        public List<Order> GetAll() =>
            _orders.Find(order => true).ToList();

        public Order GetById(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order newOrder) =>
            _orders.ReplaceOne(order => order.Id == id, newOrder);

        public void Remove(string id) =>
            _orders.DeleteOne(order => order.Id == id);      
    }
}
