using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WEBApi.Models;
using WEBApi.Services;
using System.Linq;

namespace WEBApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly DrinkService _drinkService;
        private readonly DiscountCodeService _discountCodeService;
        private readonly OrderService _orderService;

        public QueryController(DrinkService drinkService, DiscountCodeService discountCodeService, OrderService orderService)
        {
            _drinkService = drinkService;
            _discountCodeService = discountCodeService;
            _orderService = orderService;
        }

        [HttpGet("drinks/numberofaviableamount/lessthan/{amount}")]
        public ActionResult<List<Drink>> GetAllDrinks(int amount)
        {
            var drinks = _drinkService.GetAllDrinks()
                                      .Where(d => d.AviableNumbersOfDrink < amount)
                                      .ToList();

            if (drinks == null)
                return NotFound();

            return drinks;
        }


        [HttpGet("orders/{day}/{month}/{year}")]
        public ActionResult<List<Order>> GetOrdersByDate(string day, string month, string year)
        {
            string date = day + "/" + month + "/" + year;

            var orders = _orderService.GetAllOrders()
                                      .FilterByDate(date)
                                      .ToList();

            if (orders == null)
                return NotFound();

            return orders;
        }
                
        [HttpGet("orders/drinks from orders")]
        public ActionResult<List<DrinksFromOrder>> PrintDrinksFromOrders()
        {
            var orders = _orderService.GetAllOrders()
                                       .TakeDrinksFromOrder()
                                       .Select(e => e)
                                       .OrderByDescending(e => e.OrderedDrink.DrinkId)
                                       .ToList();

            if (orders == null)
                return NotFound();

            return orders;
        }

        [HttpGet("orders/drinks from orders SelectMany")]
        public ActionResult<List<OrderedDrink>> PrintDrinksFromOrdersWithSelectMany()
        {
            var orders = _orderService.GetAllOrders()
                                      .SelectMany( order => order.OrderedDrinks)
                                      .ToList();

            if (orders == null)
                return NotFound();

            return orders;
        }

        [HttpGet("orders/by drink name")]
        public ActionResult<IEnumerable<DrinksFromOrderByName>> PrintDrinksByNameFromOrder()
        {
            var orders = _orderService.GetAllOrders()
                                            .TakeDrinksFromOrder()
                                            .ToList();

            var drinks = _drinkService.GetAllDrinks();


            // start the query
            var query = orders.Join(drinks,
                                    order => order.OrderedDrink.DrinkId,
                                    drink => drink.Id,
                                    (order, drink) => new DrinksFromOrderByName
                                    (
                                        order.OrderId,
                                        drink.DrinkName,
                                        order.OrderedDrink.NumbersOfDrink
                                    ))
                              .OrderBy(e => e.DrinkName)
                              .ToList();

            /*var query2 = from order in orders
                         join drink in drinks
                             on order.OrderedDrink.DrinkId equals drink.Id
                         orderby drink.DrinkName
                         select new DrinksFromOrderByName
                         (
                             order.OrderId,
                             drink.DrinkName,
                             order.OrderedDrink.NumbersOfDrink
                         );*/

            if (query == null)
                return NotFound();

            return query;
        }

        //-------------Not printing in the browser--------------------------
        [HttpGet("orders/total numbers of order from each drink")]
        public ActionResult<List<OrderedDrink>> TotalNumbersOfOrderFromEachDrink()
        {
            var orders = _orderService.GetAllOrders()
                                      .SelectMany(e => e.OrderedDrinks)
                                      .ToList();

            var drinks = _drinkService.GetAllDrinks();

            var query = orders.Join(drinks,
                                    o => o.DrinkId,
                                    d => d.Id,
                                    (o, d) => new OrderedDrinkWithName
                                    (
                                        d.DrinkName,
                                        o.NumbersOfDrink
                                    ))
                              .GroupBy(e => e.DrinkName)
                              .Select(g =>
                                            {
                                                var result = g.Aggregate(new DrinkStatistic(), (e, c) => e.FindTotal(c));
                                                return new
                                                {
                                                    Name = g.Key,
                                                    NumbersOfTotalOrder = result.Total
                                                };
                                            })
                              .OrderByDescending(r => r.NumbersOfTotalOrder).ToList();

            foreach (var order in query)
            {
                Console.WriteLine(order.Name + " : " + order.NumbersOfTotalOrder);
            }

            if (query == null)
                return NotFound();

            return NoContent();
        }


        //-------------Not printing in the browser--------------------------
        [HttpGet("orders/groupbydrink")]
        public ActionResult<IEnumerable<IGrouping<string, OrderedDrink>>> PrintGroupByDrinksFromOrder()
        {
            var orders = _orderService.GetAllOrders()
                                       .SelectMany(o => o.OrderedDrinks)
                                       .GroupBy(o => o.DrinkId)
                                       .OrderBy(g => g.Key);

            /*foreach (var order in orders)
            {
                Console.WriteLine(order.Key);
                foreach (var drink in order)
                    Console.WriteLine(drink.NumbersOfDrink);
            }*/

            if (orders == null)
                return NotFound();

            return NoContent();
        }

    }
}
