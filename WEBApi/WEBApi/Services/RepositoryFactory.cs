using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi.Services
{
    public static class RepositoryFactory
    {
        public static object GetRepository(string repositoryType)
        {
            switch(repositoryType)
            {
                case "drink":
                    return new DrinkService((IMongoCollection<Drink>)GetCollectionByName("drinks"));
                case "discountCode":
                    return new DiscountCodeService();
                case "order":
                    return new OrderService();
                default:
                    throw new ArgumentException("Invalid repository type!!!!!!!");
            }
        }

        public static object GetCollectionByName(string collectionName)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MyDB");
            switch (collectionName)
            {
                case "drinks":
                    IMongoCollection<Drink> collectionDrinks = database.GetCollection<Drink>("Drinks");
                    return collectionDrinks;
                case "discountCodes":
                    IMongoCollection<DiscountCode> collectionDiscountCodes = database.GetCollection<DiscountCode>("DiscountCodes");
                    return collectionDiscountCodes;
                case "orders":
                    IMongoCollection<Order> collectionOrders = database.GetCollection<Order>("Orders");
                    return collectionOrders;
                default:
                    throw new ArgumentException("Invalid collection name!!!!!!!");
            }
        }

    }
}
