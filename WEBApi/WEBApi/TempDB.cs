using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi
{
    public class TempDB
    {
        public List<Drink> drinks;

        public TempDB()
        {
            drinks = new List<Drink>();
            drinks.Add(new Drink ( "Italian coffee", 100, 2.50 ));
            drinks.Add(new Drink ("American coffee", 90, 2.00));
            drinks.Add(new Drink ("Tea", 100, 1.50));
            drinks.Add(new Drink ("Tea", 80, 1.30));
            drinks.Add(new Drink("Chocolate", 100, 2.00));
        }
    }
}
