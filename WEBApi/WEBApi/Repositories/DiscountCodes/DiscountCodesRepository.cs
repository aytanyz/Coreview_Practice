using WEBApi.Models;
using WEBApi.Repositories.Base;

namespace WEBApi.Repositories.DiscountCodes
{
    public class DiscountCodesRepository : Repository<DiscountCode>, IDiscountCodesRepository
    {
        public DiscountCodesRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
        }
    }
}
