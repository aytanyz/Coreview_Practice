using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public class DrinkStatistic
    {
        public int Total { get; set; }

        public DrinkStatistic()
        {

        }

        public DrinkStatistic FindTotal(OrderedDrinkWithName drink)
        {
            Total += drink.NumbersOfDrinks;
            return this;
        }
    }
}
