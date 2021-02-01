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
                
        [HttpGet("orders/drinksfromorders")]
        public ActionResult<List<DrinksFromOrder>> PrintDrinksFromOrders()
        {
            /*var orders = _orderService.GetAllOrders()
                                      .Select( order => order.OrderedDrinks.Select(drink => new DrinksFromOrder(drink, order.Id)))
                                      .ToList();*/

            var orders = _orderService.GetAllOrders()
                                       .TakeDrinksFromOrder()
                                       .Select(e => e)
                                       .OrderByDescending(e => e.OrderedDrink.DrinkId)
                                       .ToList();

            if (orders == null)
                return NotFound();

            return orders;
        }

        [HttpGet("orders/bydrinkname")]
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

            var query2 = from order in orders
                         join drink in drinks
                             on order.OrderedDrink.DrinkId equals drink.Id
                         orderby drink.DrinkName
                         select new DrinksFromOrderByName
                         (
                             order.OrderId,
                             drink.DrinkName,
                             order.OrderedDrink.NumbersOfDrink
                         );

            if (query2 == null)
                return NotFound();

            return query2.ToList();
        }


        //-------------Not working--------------------------
        [HttpGet("orders/groupbydrink")]
        public ActionResult<IEnumerable<IGrouping<string, DrinksFromOrder>>> PrintGroupByDrinksFromOrder()
        {
            var orders = _orderService.GetAllOrders()
                                       .TakeDrinksFromOrder()
                                       .Select(e => e)
                                       .GroupBy(e => e.OrderedDrink.DrinkId, e => e)
                                       .ToList();

            if (orders == null)
                return NotFound();

            return NoContent();
        }

    }
}
