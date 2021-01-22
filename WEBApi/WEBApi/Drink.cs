using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi
{
    public class Drink
    {
        //public Guid DrinkId { get; set; }
        public int DrinkId { get; set; }
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
