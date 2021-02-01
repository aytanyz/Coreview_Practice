using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public class DrinksFromOrder
    {
        public string OrderId { get; set; }
        public OrderedDrink OrderedDrink { get; set; }

        public DrinksFromOrder()
        {
        }        

        public DrinksFromOrder(string orderId, OrderedDrink orderedDrink)
        {
            OrderedDrink = orderedDrink;
            OrderId = orderId;
        }
    }
}
