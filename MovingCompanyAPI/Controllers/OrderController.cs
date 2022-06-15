using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovingCompanyAPI.Models;
using MovingCompanyAPI.Services;

namespace MovingCompanyAPI.Controllers.V2
{
    public class Data
    {
        public string message { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class OrderController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<Data> Get()
        {
            return new Data { message = "Example API version" };
        }
    }

}

namespace MovingCompanyAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Order>> GetAll() => OrderService.GetAll();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Order> Get(int id)
        {
            var order = OrderService.Get(id);

            if (order == null) return NotFound();

            System.Diagnostics.Debug.WriteLine(order);

            return order;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Order Order)
        {
            Order.UpdateDate = DateTime.Now;
            OrderService.Add(Order);
            return CreatedAtAction(nameof(Create), new { id = Order.Id }, Order);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Order Order)
        {
            if (id != Order.Id)
                return BadRequest();

            var existingOrder = OrderService.Get(id);
            if (existingOrder is null)
                return NotFound();

            Order.UpdateDate = DateTime.Now;
            OrderService.Update(Order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var Order = OrderService.Get(id);

            if (Order is null)
                return NotFound();

            OrderService.Delete(id);

            return NoContent();
        }
    }

}