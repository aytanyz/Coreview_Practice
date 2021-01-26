using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi
{
    public static class TempDB
    {
        public static List<Drink> drinks;
        public static DrinkManager drinkManager;

        static TempDB()
        {
            drinks = new List<Drink>();
            drinkManager = new DrinkManager();

            drinkManager.AddDrink("Italian coffee", 100, 2.50 );
            drinkManager.AddDrink ("American coffee", 90, 2.00);
            drinkManager.AddDrink ("Tea", 100, 1.50);
            drinkManager.AddDrink ("Tea", 80, 1.30);
            drinkManager.AddDrink("Chocolate", 100, 2.00);
        }
    }
}
