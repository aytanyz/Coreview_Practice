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
    public class OrdersMongoDBController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersMongoDBController(OrderService orderService)
        {
            _orderService = orderService;
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
        public ActionResult<Order> Create(Order order)
        {
            _orderService.Create(order);

            return CreatedAtRoute("GetDrink", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order newOrder)
        {
            var book = _orderService.GetOrderById(id);

            if (book == null)
            {
                return NotFound();
            }

            _orderService.Update(id, newOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.RemoveById(order.Id);

            return NoContent();
        }
    }
}
