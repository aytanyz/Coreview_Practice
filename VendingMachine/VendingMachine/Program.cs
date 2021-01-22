using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDB();            
        }

        public static void CreateDB()
        {
            TempDB db = new TempDB();

            db.AddDrink("Italian Coffee", 2.5, "icon1");
            db.AddDrink("American Coffee", 2, "icon2");
            db.AddDrink("Tea", 1.5, "icon3");
            db.AddDrink("Chocolate", 2.4, "icon4");

            db.AddDiscountCode("promo15", 15);
            db.AddDiscountCode("promo20", 20);
            db.AddDiscountCode("promo50", 50);
        }
    }
}
