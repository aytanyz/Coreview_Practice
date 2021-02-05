using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;


namespace WEBApi.Services
{
    public class DiscountCodeService : IServiceRepository<DiscountCode>
    {
        private readonly IMongoCollection<DiscountCode> _discountCodes;
   
        public DiscountCodeService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MyDB");

            _discountCodes = database.GetCollection<DiscountCode>("DiscountCodes");
        }

        public List<DiscountCode> GetAll() =>
            _discountCodes.Find(discountCode => true).ToList();

        public DiscountCode GetById(string id) =>
            _discountCodes.Find<DiscountCode>(discountCode => discountCode.Id == id).FirstOrDefault();

        public DiscountCode Create(DiscountCode discountCode)
        {
            _discountCodes.InsertOne(discountCode);
            return discountCode;
        }

        public void Update(string id, DiscountCode newDiscountCode) =>
            _discountCodes.ReplaceOne(discountCode => discountCode.Id == id, newDiscountCode);

        public void Remove(string id) =>
            _discountCodes.DeleteOne(discountCode => discountCode.Id == id);

    }
}
