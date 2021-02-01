using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WEBApi.Models;

namespace WEBApi.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>("Orders");
        }

        public List<Order> GetAllOrders() =>
            _orders.Find(order => true).ToList();

        public Order GetOrderById(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void UpdateOrder(string id, Order newOrder) =>
            _orders.ReplaceOne(order => order.Id == id, newOrder);

        public void RemoveOrderById(string id) =>
            _orders.DeleteOne(order => order.Id == id);
    }
}
