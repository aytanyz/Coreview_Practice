using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public class OrderedDrinkWithName
    {
        public string DrinkName { get; set; }
        public int NumbersOfDrinks { get; set; }

        public OrderedDrinkWithName()
        {

        }

        public OrderedDrinkWithName(string drinkName, int numbersOfDrinks)
        {
            DrinkName = drinkName;
            NumbersOfDrinks = numbersOfDrinks;
        }
    }
}
