using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi
{
    public class DrinkManager
    {
        public List<Drink> Drinks { get; set; }
        private int ID = 0;
        public DrinkManager()
        {
            Drinks = new List<Drink>();
        }

        public void AddDrink(string drinkName, int aviableAmount, double drinkPrice)
        {
            Drink drink = new Drink();
            drink.DrinkName = drinkName;
            drink.AviableAmount = aviableAmount;
            drink.DrinkPrice = drinkPrice;
            drink.DrinkId = ID;
            ID++;
            Drinks.Add(drink);
        }

        public bool CheckDrink(int drinkId)
        {
            foreach (var drink in Drinks)
            {
                if (drink.DrinkId == drinkId)
                    return true;
            }
            return false;
        }

        public Drink GetDink(int drinkId)
        {
            //fix this method
            try
            {
                foreach (var drink in Drinks)
                {
                    if (drink.DrinkId == drinkId)
                        return drink;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}
