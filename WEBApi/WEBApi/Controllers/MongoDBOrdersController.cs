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
        private readonly IServiceRepository<Order> _orderService;
        private readonly IServiceRepository<Drink> _drinkService;

        public MongoDBOrdersController(OrderService orderService, DrinkService drinkService)
        {
            _orderService = orderService;
            _drinkService = drinkService;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders() =>
            _orderService.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            order.OrderStatus = Status.Created;

            if (order.DiscountCodeId != null)
                order.DiscountCodeUsed = true;
            else
                order.DiscountCodeUsed = false;

            // creating new order
            _orderService.Create(order);

            // we decrease the aviable number of ordered drinks in stock
            foreach (var drink in order.OrderedDrinks)
            {
                var drinkToEdit = _drinkService.GetById(drink.DrinkId);
                drinkToEdit.AviableNumbersOfDrink -= drink.NumbersOfDrink;
                _drinkService.Update(drink.DrinkId, drinkToEdit);
            }

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order newOrder)
        {
            var book = _orderService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            _orderService.Update(id, newOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}/canceled")]
        public IActionResult CanceleOrder(string id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            // we increase aviable number of drink in stock as the order was deleted.
            foreach (var drink in order.OrderedDrinks)
            {
                var drinkToEdit = _drinkService.GetById(drink.DrinkId);
                drinkToEdit.AviableNumbersOfDrink += drink.NumbersOfDrink;
                _drinkService.Update(drink.DrinkId, drinkToEdit);
            }

            // _orderService.RemoveOrderById(order.Id);
            order.OrderStatus = Status.Canceled;
            _orderService.Update(id, order);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteOrder(string id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order.Id);

            return NoContent();
        }
    }
}
