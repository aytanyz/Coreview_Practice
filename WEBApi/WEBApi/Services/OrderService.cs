using System.Collections.Generic;
using System.Linq;
using WEBApi.Models;
using WEBApi.Repositories.Orders;


namespace WEBApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public List<Order> GetAll() =>
            _ordersRepository.GetAll();

        public Order GetById(string id) =>
            _ordersRepository.GetById(id);

        public void Create(Order order)
        {
            _ordersRepository.Create(order);
        }

        public void Update(string id, Order newOrder) =>
            _ordersRepository.Update(id, newOrder);

        public void Remove(string id) =>
            _ordersRepository.Remove(id);
    }
}
