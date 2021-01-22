using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public interface IDrinkManager
    {
        void UpdateDrinkPrice(double price);
        void UpdateDrinkAviableAmount(int usedAmount);
        void RemoveDrink(Guid DrinkId);
    }
}
