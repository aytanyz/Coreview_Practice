using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;
using WEBApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MongoDBOrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly DrinkService _drinkService;

        public MongoDBOrdersController(OrderService orderService, DrinkService drinkService)
        {
            _orderService = orderService;
            _drinkService = drinkService;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders() =>
            _orderService.GetAllOrders();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            order.OrderStatus = Status.Created;

            if (order.DiscountCodeId != null)
                order.DiscountCodeUsed = true;
            else
                order.DiscountCodeUsed = false;

            // creating new order
            _orderService.CreateOrder(order);

            // we decrease the aviable number of ordered drinks in stock
            foreach (var drink in order.OrderedDrinks)
            {
                _drinkService.DecreaseDrinkAviabilityInStock(drink.DrinkId, drink.NumbersOfDrink);
            }

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order newOrder)
        {
            var book = _orderService.GetOrderById(id);

            if (book == null)
            {
                return NotFound();
            }

            _orderService.UpdateOrder(id, newOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}/canceled")]
        public IActionResult CanceleOrder(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            // we increase aviable number of drink in stock as the order was deleted.
            foreach (var drink in order.OrderedDrinks)
            {
                _drinkService.IncreaseDrinkAviabilityInStock(drink.DrinkId, drink.NumbersOfDrink);
            }

            // _orderService.RemoveOrderById(order.Id);
            order.OrderStatus = Status.Canceled;
            _orderService.UpdateOrder(id, order);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteOrder(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.RemoveOrderById(order.Id);

            return NoContent();
        }
    }
}
