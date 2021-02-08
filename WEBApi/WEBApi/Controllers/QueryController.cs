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
        private readonly IDrinkService _drinkService;
        private readonly IOrderService _orderService;

        public QueryController(IDrinkService drinkService, IOrderService orderService)
        {
            _drinkService = drinkService;
            _orderService = orderService;
        }

        [HttpGet("drinks/numberofaviableamount/lessthan/{amount}")]
        public ActionResult<List<Drink>> GetAllDrinks(int amount)
        {
            var drinks = _drinkService.GetAll()
                                      .Where(d => d.AviableNumbersOfDrink < amount)
                                      .ToList();

            if (drinks.Count==0)
                return NotFound();

            return drinks;
        }


        [HttpGet("orders/{day}/{month}/{year}")]
        public ActionResult<List<Order>> GetOrdersByDate(string day, string month, string year)
        {
            string date = day + "/" + month + "/" + year;

            var orders = _orderService.GetAll()
                                      .FilterByDate(date)
                                      .ToList();

            if (orders.Count == 0)
                return NotFound();

            return orders;
        }
                
        [HttpGet("orders/drinks_from_orders")]
        public ActionResult<List<DrinksFromOrder>> PrintDrinksFromOrders()
        {
            var orders = _orderService.GetAll()
                                       .TakeDrinksFromOrder()
                                       .Select(e => e)
                                       .OrderByDescending(e => e.OrderedDrink.DrinkId)
                                       .ToList();

            if (orders.Count == 0)
                return NotFound();

            return orders;
        }

        [HttpGet("orders/drinks_from_orders_selectmany")]
        public ActionResult<List<OrderedDrink>> PrintDrinksFromOrdersWithSelectMany()
        {
            var orders = _orderService.GetAll()
                                      .SelectMany( order => order.OrderedDrinks)
                                      .ToList();

            if (orders.Count == 0)
                return NotFound();

            return orders;
        }

        [HttpGet("orders/by_drink_name")]
        public ActionResult<IEnumerable<DrinksFromOrderByName>> PrintDrinksByNameFromOrder()
        {
            var orders = _orderService.GetAll()
                                            .TakeDrinksFromOrder()
                                            .ToList();

            var drinks = _drinkService.GetAll();


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

            if (query.Count == 0)
                return NotFound();

            return query;
        }

        //-------------Not printing in the browser--------------------------
        [HttpGet("orders/groupbydrink")]
        public ActionResult<IEnumerable<IGrouping<string, OrderedDrink>>> PrintGroupByDrinksFromOrder()
        {
            var orders = _orderService.GetAll()
                                       .SelectMany(o => o.OrderedDrinks)
                                       .GroupBy(o => o.DrinkId)
                                       .OrderBy(g => g.Key);

            /*foreach (var order in orders)
            {
                Console.WriteLine(order.Key);
                foreach (var drink in order)
                    Console.WriteLine(drink.NumbersOfDrink);
            }*/

            if (orders.ToList().Count == 0)
                return NotFound();

            return NoContent();
        }
/*
        //-------------Not printing in the browser--------------------------
        [HttpGet("orders/totalnumbersoforderfromeachdrink")]
        //public ActionResult<List<(string, int)>> TotalNumbersOfOrderFromEachDrink()
        public ActionResult<List<(string, int)>> TotalNumbersOfOrderFromEachDrink()
        {
            var orders = _orderService.GetAll()
                                      .SelectMany(e => e.OrderedDrinks)
                                      .ToList();

            var drinks = _drinkService.GetAll();

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
                                                (string Name, int Total) drinkList = (g.Key, result.Total);
                                                return drinkList;
                                            })
                              .OrderByDescending(r => r.Total)
                              .ToList();

            foreach (var order in query)
            {
                Console.WriteLine(order.Name + " : " + order.Total);
            }

            if (query.Count == 0)
                return NotFound();

            return query;
        }*/

    }
}
