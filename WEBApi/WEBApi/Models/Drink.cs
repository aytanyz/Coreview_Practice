using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace WEBApi.Models
{
    public class Drink
    {
        //public Guid DrinkId { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int AviableNumbersOfDrink { get; set; }
        public double DrinkPrice { get; set; }

        public Drink()
        {

        }
        public Drink(string drinkName, int aviableNumbersOfDrink, double price)
        {
            DrinkName = drinkName;
            AviableNumbersOfDrink = aviableNumbersOfDrink;
            DrinkPrice = price;
            //DrinkId = Guid.NewGuid();
        }
    }
}
