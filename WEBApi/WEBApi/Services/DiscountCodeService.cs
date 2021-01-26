using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;


namespace WEBApi.Services
{
    public class DiscountCodeService
    {
        private readonly IMongoCollection<DiscountCode> _discountCodes;
   
        public DiscountCodeService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _discountCodes = database.GetCollection<DiscountCode>(settings.CollectionName[1]);
        }

        public List<DiscountCode> GetAllDiscountCodes() =>
            _discountCodes.Find(discountCode => true).ToList();

        public DiscountCode GetDiscountCodeById(string id) =>
            _discountCodes.Find<DiscountCode>(discountCode => discountCode.Id == id).FirstOrDefault();

        public DiscountCode Create(DiscountCode discountCode)
        {
            _discountCodes.InsertOne(discountCode);
            return discountCode;
        }

        public void Update(string id, DiscountCode newDiscountCode) =>
            _discountCodes.ReplaceOne(discountCode => discountCode.Id == id, newDiscountCode);

        public void RemoveByDiscountCode(DiscountCode discountCodeToDelete) =>
            _discountCodes.DeleteOne(discountCode => discountCode.Id == discountCodeToDelete.Id);

        public void RemoveById(string id) =>
            _discountCodes.DeleteOne(discountCode => discountCode.Id == id);
    }
}
