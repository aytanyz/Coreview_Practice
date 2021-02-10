using System;
using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Models
{
    public class Order : EntityBase
    {

        public List<OrderedDrink> OrderedDrinks { get; set; }
        public DateTime Date { get; private set; }
        public string DiscountCodeId { get; set; }

        public Order(List<OrderedDrink> orderedDrinks, string discountCodeId)
        {
            OrderedDrinks = orderedDrinks;
            DiscountCodeId = discountCodeId;
            Date = DateTime.Today;
        }

        public Order()
        {
            Date = DateTime.Today;
        }
    }
}
