using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WEBApi.Models
{
    [Serializable]
    public class Order
    {
        private bool _discountCodeIsUsed;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<OrderedDrink> OrderedDrinks { get; set; }
        public DateTime Date { get; private set; }
        public Status OrderStatus { get; set; }
        public string DiscountCodeId { get; set; }
        public bool DiscountCodeUsed
        {
            get 
            { 
                return _discountCodeIsUsed;  
            }
            set 
            { 
                _discountCodeIsUsed = value; 
            }
        }

        public Order()
        {
            _discountCodeIsUsed = false;
            Date = DateTime.Today;
        }
    }
}
