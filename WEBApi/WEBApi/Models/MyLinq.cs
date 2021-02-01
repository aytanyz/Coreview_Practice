using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public static class MyLinq
    {
        /*public static IEnumerable<T> FilterByDate<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var s in source)
            {
                if (predicate(s))
                    yield return s;
            }
        }*/

        public static IEnumerable<Order> FilterByDate(this List<Order> source, string date)
        {
            foreach (var s in source)
            {
                if (s.Date.ToString("dd/MM/yyyy") == date)
                    yield return s;
            }
        }

        public static IEnumerable<DrinksFromOrder> TakeDrinksFromOrder(this List<Order> source)
        {
            foreach (var order in source)
            {
                foreach (var drink in order.OrderedDrinks)
                {
                    yield return new DrinksFromOrder(order.Id, new OrderedDrink(drink.DrinkId, drink.NumbersOfDrink)); 
                }
            }
        }
    }
}
