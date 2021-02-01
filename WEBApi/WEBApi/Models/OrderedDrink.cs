using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public class OrderedDrink
    {
        public string DrinkId { get; set; }
        public int NumbersOfDrink { get; set; }

        public OrderedDrink()
        {

        }

        public OrderedDrink(string drinkId, int numbersOfDrink)
        {
            DrinkId = drinkId;
            NumbersOfDrink = numbersOfDrink;
        }
    }
}
