using System;
using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Models
{
    public class Order : EntityBase
    {
        private bool _discountCodeIsUsed;

        public List<OrderedDrink> OrderedDrinks { get; set; }
        public DateTime Date { get; private set; }
        public string DiscountCodeId { get; set; }

        public Order()
        {
            _discountCodeIsUsed = false;
            Date = DateTime.Today;
        }
    }
}
