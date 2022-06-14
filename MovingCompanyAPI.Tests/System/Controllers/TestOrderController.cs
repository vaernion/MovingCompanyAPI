using System.Net;
using MovingCompanyAPI.Controllers;
using MovingCompanyAPI.Models;
using MovingCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovingCompanyAPI.Tests;

public class TestOrderController
{
    private List<Order> GetTestOrders()
    {
        return OrderService.GenerateExampleOrders();
    }

    [Fact]
    public void GetAllOrders_Works()
    {
        var orderController = new OrderController();

        var result = orderController.GetAll();

        Assert.IsType<List<Order>>(result.Value);
    }

    [Fact]
    public void GetOrder_WorksWithValidId()
    {
        var orderController = new OrderController();
        Order expected = GetTestOrders().First(order => order.Id == 1);

        var result = orderController.Get(expected.Id);

        if (result.Value == null)
            throw new Exception("Order was not retrieved");

        Order value = result.Value;

        Assert.IsType<Order>(result.Value);
        // can't directly compare because of type differences
        // Assert.Equal(expected, value);
        // Assert.Equal(expected.Customer, value.Customer);

        Assert.Equal(expected.Id, value.Id);
        Assert.Equal(expected.OrderDate, value.OrderDate);

        // workaround for nonworking Assert.Equal on ActionResponse objects
        foreach (var prop in value.Customer.GetType().GetProperties())
        {
            var expectedCustomerProperty = prop.GetValue(value.Customer);
            var actualCustomerProperty = prop.GetValue(expected.Customer);
            Assert.Equal(expectedCustomerProperty, actualCustomerProperty);
        }
    }

    [Fact]
    public void GetOrder_FailsWithNonexistingId()
    {
        var orderController = new OrderController();

        var result = orderController.Get(-1);

        Assert.IsNotType<Order>(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void CreateOrder_Works()
    {
        var orderController = new OrderController();
        var newOrder = new Order
        {
            Id = 4,
            IsDone = false,
            UpdateDate = DateTime.Now,
            OrderDate = new DateTime(2023, 01, 01),
            AddressFrom = "A",
            AddressTo = "B",
            Note = "",
            Services = new OrderServices
            {
                IsMoving = true,
                IsPacking = true,
                IsCleaning = true
            },
            Customer = new Customer
            {
                Name = "Unit Test",
                Email = "unit.test@gmail.com",
                Phone = "99999999"
            }
        };

        dynamic result = orderController.Create(newOrder);
        int newId = result.Value.Id;

        Assert.IsType<CreatedAtActionResult>(result);

        var addedOrder = orderController.Get(newId);
        if (addedOrder.Value == null)
            throw new Exception("Order was not retrieved");

        Assert.Equal(newOrder.OrderDate, result.Value.OrderDate);
        Assert.Equal(newOrder.Customer.Email, addedOrder.Value.Customer.Email);
    }

    [Fact]
    public void UpdateOrder_Works()
    {
        var orderController = new OrderController();
        var orderWithChanges = new Order
        {
            Id = 2,
            IsDone = false,
            UpdateDate = DateTime.Now,
            OrderDate = new DateTime(2023, 01, 01),
            AddressFrom = "A",
            AddressTo = "B",
            Note = "",
            Services = new OrderServices
            {
                IsMoving = true,
                IsPacking = true,
                IsCleaning = true
            },
            Customer = new Customer
            {
                Name = "Unit Test",
                Email = "unit.test@gmail.com",
                Phone = "99999999"
            }
        };

        dynamic result = orderController.Update(orderWithChanges.Id, orderWithChanges);

        Assert.IsType<NoContentResult>(result);

        var updatedOrder = orderController.Get(orderWithChanges.Id);
        if (updatedOrder.Value == null)
            throw new Exception("Order was not retrieved");

        Assert.Equal(orderWithChanges.OrderDate, updatedOrder.Value.OrderDate);
        Assert.Equal(orderWithChanges.Customer.Email, updatedOrder.Value.Customer.Email);
    }

    [Fact]
    public void DeleteOrder_Works()
    {
        var orderController = new OrderController();

        var result = orderController.Delete(4);

        Assert.IsNotType<Order>(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteOrder_FailsWithNonexistingId()
    {
        var orderController = new OrderController();

        var result = orderController.Delete(-1);

        Assert.IsNotType<Order>(result);
        Assert.IsType<NotFoundResult>(result);
    }
}