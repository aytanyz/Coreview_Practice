using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string[] DrinkId { get; set; }
        public string[] DrinkName { get; set; }
        public string[] DrinkAmount { get; set; }
        public bool DiscountCodeUsed { get; set; }
        public string DiscoundCode { get; set; }
    }
}
