using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Drink
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string IconName { get; set; }
        public int AviableAmount { get; set; }
        public Guid DrinkId { get; set; }
        //public Guid Id2 => Guid.NewGuid();


        public Drink(string name, double price, string iconName, int aviableAmount)
        {
            DrinkId = Guid.NewGuid();
            Name = name;
            Price = price;
            IconName = iconName;
            AviableAmount = aviableAmount;

        }
    }
}
