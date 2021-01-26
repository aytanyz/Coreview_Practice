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
        //[BsonElement("Name")]
        public string DrinkName { get; set; }
        public int AviableAmount { get; set; }
        public double DrinkPrice { get; set; }

        public Drink()
        {

        }
        public Drink(string drinkName, int aviableAmount, double price)
        {
            DrinkName = drinkName;
            AviableAmount = aviableAmount;
            DrinkPrice = price;
            //DrinkId = Guid.NewGuid();
        }
    }
}
