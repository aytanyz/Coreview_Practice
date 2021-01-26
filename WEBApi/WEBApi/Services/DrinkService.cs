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

            _drinks = database.GetCollection<Drink>(settings.CollectionName[0]);
        }

        public List<Drink> GetAllDrinks() =>
            _drinks.Find(drink => true).ToList();

        public Drink GetDrinkById(string id) =>
            _drinks.Find<Drink>(drink => drink.Id == id).FirstOrDefault();

        public Drink Create(Drink drink)
        {
            _drinks.InsertOne(drink);
            return drink;
        }

        public void Update(string id, Drink newDrink) =>
            _drinks.ReplaceOne(drink => drink.Id == id, newDrink);

        public void RemoveByDrink(Drink drinkToDelete) =>
            _drinks.DeleteOne(drink => drink.Id == drinkToDelete.Id);

        public void RemoveById(string id) =>
            _drinks.DeleteOne(drink => drink.Id == id);
    }
}
