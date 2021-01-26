using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBApi.Models
{
    public class DiscountCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
        //public bool IsUsed { get; set; }

        public DiscountCode()
        {

        }
        public DiscountCode(string code, int discountPercentage)
        {
            Code = code;
            DiscountPercentage = discountPercentage;
        }
    }
}
