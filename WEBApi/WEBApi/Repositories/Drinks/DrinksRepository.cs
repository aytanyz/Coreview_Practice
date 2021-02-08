using WEBApi.Models;
using WEBApi.Repositories.Base;

namespace WEBApi.Repositories.Drinks
{
    public class DrinksRepository : Repository<Drink>, IDrinksRepository
    {
        public DrinksRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
        }
    }
}
