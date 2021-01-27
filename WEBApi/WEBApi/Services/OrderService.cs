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

            _orders = database.GetCollection<Order>(settings.CollectionName[2]);
        }

        public List<Order> GetAllOrders() =>
            _orders.Find(order => true).ToList();

        public Order GetOrderById(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order newOrder) =>
            _orders.ReplaceOne(order => order.Id == id, newOrder);

        public void RemoveByOrder(Order orderToDelete) =>
            _orders.DeleteOne(order => order.Id == orderToDelete.Id);

        public void RemoveById(string id) =>
            _orders.DeleteOne(order => order.Id == id);
    }
}
