using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class TempDB
    {
        public List<Drink> Drinks { get; set; }
        public List<DiscountCode> DiscountCodes { get; set; }

        public TempDB()
        {
            Drinks = new List<Drink>();
            DiscountCodes = new List<DiscountCode>();
        }

        public void AddDrink(string name, double price, string IconName)
        {
            var drink = new Drink(name, price, IconName);           
            Drinks.Add(drink);            
        }

        public void AddDiscountCode(string code, int percentage)
        {
            var discountCode = new DiscountCode(code, percentage);
            DiscountCodes.Add(discountCode);
        }

        public void UpdatePrice()
        {

        }


    }
}
