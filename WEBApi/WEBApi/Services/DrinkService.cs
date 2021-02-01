using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi.Services
{
    public class DrinkService
    {
        private readonly IMongoCollection<Drink> _drinks;

        public DrinkService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _drinks = database.GetCollection<Drink>("Drinks");
        }

        public List<Drink> GetAllDrinks() =>
            _drinks.Find(drink => true).ToList();

        public Drink GetDrinkById(string id) =>
            _drinks.Find<Drink>(drink => drink.Id == id).FirstOrDefault();

        public Drink CreateDrink(Drink drink)
        {
            _drinks.InsertOne(drink);
            return drink;
        }

        public void UpdateDrink(string id, Drink newDrink) =>
            _drinks.ReplaceOne(drink => drink.Id == id, newDrink);

        public void DecreaseDrinkAviabilityInStock(string id, int amount)
        {
            var drink = GetDrinkById(id);
            drink.AviableNumbersOfDrink -= amount;
            _drinks.ReplaceOne(drink => drink.Id == id, drink);
        }

        public void IncreaseDrinkAviabilityInStock(string id, int amount)
        {
            var drink = GetDrinkById(id);
            drink.AviableNumbersOfDrink += amount;
            _drinks.ReplaceOne(drink => drink.Id == id, drink);
        }

        public void RemoveDrinkById(string id) =>
            _drinks.DeleteOne(drink => drink.Id == id);
    }
}
