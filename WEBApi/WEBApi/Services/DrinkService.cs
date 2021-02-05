using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IMongoCollection<Drink> _drinks; 

        public DrinkService(IMongoCollection<Drink> drink)
        {
            _drinks = drink;
        }

        public List<Drink> GetAll() =>
            _drinks.Find(drink => true).ToList();

        public Drink GetById(string id) =>
            _drinks.Find<Drink>(drink => drink.Id == id).FirstOrDefault();

        public Drink Create(Drink drink)
        {
            _drinks.InsertOne(drink);
            return drink;
        }

        public void Update(string id, Drink newDrink) =>
            _drinks.ReplaceOne(drink => drink.Id == id, newDrink);

        public void Remove(string id) =>
            _drinks.DeleteOne(drink => drink.Id == id);

    }
}
