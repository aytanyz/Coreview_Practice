using WEBApi.Models;
using WEBApi.Repositories.Base;

namespace WEBApi.Repositories.Orders
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
        }
    }
}
