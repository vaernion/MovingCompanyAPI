using MovingCompanyAPI.Models;
using MovingCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovingCompanyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    public OrderController()
    {
    }

    [HttpGet]
    public ActionResult<List<Order>> GetAll() => OrderService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Order> Get(int id)
    {
        var order = OrderService.Get(id);

        if (order == null) return NotFound();

        System.Diagnostics.Debug.WriteLine(order);

        return order;
    }

    [HttpPost]
    public IActionResult Create(Order Order)
    {
        Order.UpdateDate = DateTime.Now;
        OrderService.Add(Order);
        return CreatedAtAction(nameof(Create), new { id = Order.Id }, Order);
    }

    [HttpPut("{id}")]
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
    public IActionResult Delete(int id)
    {
        var Order = OrderService.Get(id);

        if (Order is null)
            return NotFound();

        OrderService.Delete(id);

        return NoContent();
    }
}