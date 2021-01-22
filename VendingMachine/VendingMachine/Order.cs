using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Order
    {
        public Drink DrinkName { get; set; }
        public int Amount { get; set; }
        public Guid OrderId { get; set; }
        public bool DiscountCodeUsed { get; set; }

        public Order (Drink drinkName, int amount, bool discountCodeUsed)
        {
            OrderId = Guid.NewGuid();
            DrinkName = drinkName;
            Amount = amount;
            DiscountCodeUsed = discountCodeUsed;
        }

    }
}
