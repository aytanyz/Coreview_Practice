using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBApi.Models
{
    public class DiscountCode : EntityBase
    {
        public string Code { get; set; }
        public int DiscountPercentage { get; set; }

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
