using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Services
{
    public interface IOrderService
    {
        public List<Order> GetAll();

        public Order GetById(string id);

        public void Create(Order item);

        public void Update(string id, Order item);

        public void Remove(string id);
    }
}
