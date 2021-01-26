using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi
{
    public class DrinkManager
    {
        private int _id;
        public DrinkManager()
        {
            _id = 0;
        }

        public void AddDrink(string drinkName, int aviableAmount, double drinkPrice)
        {
            Drink drink = new Drink();
            drink.DrinkName = drinkName;
            drink.AviableAmount = aviableAmount;
            drink.DrinkPrice = drinkPrice;
            drink.DrinkId = _id;            
            TempDB.drinks.Add(drink);

            _id++;
        }

        public bool CheckDrink(int drinkId)
        {
            foreach (var drink in TempDB.drinks)
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
                foreach (var drink in TempDB.drinks)
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
