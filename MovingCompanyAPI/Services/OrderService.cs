using MovingCompanyAPI.Models;

namespace MovingCompanyAPI.Services;

public static class OrderService
{
    static List<Order> Orders { get; }
    static int nextId = 4;

    static OrderService()
    {
        Orders = GenerateExampleOrders();
    }

    public static List<Order> GetAll() => Orders;

    public static Order? Get(int id) => Orders.FirstOrDefault(p => p.Id == id);

    public static void Add(Order Order)
    {
        Order.Id = nextId++;
        Orders.Add(Order);
    }

    public static void Delete(int id)
    {
        var Order = Get(id);
        if (Order is null)
            return;

        Orders.Remove(Order);
    }

    public static void Update(Order Order)
    {
        var index = Orders.FindIndex(p => p.Id == Order.Id);
        if (index == -1)
            return;

        Orders[index] = Order;
    }

    public static List<Order> GenerateExampleOrders()
    {
        var orders = new List<Order>();

        orders.Add(
            new Order
            {
                Id = 1,
                IsDone = true,
                UpdateDate = new DateTime(2022, 05, 01),
                OrderDate = new DateTime(2022, 05, 25, 14, 30, 0),
                AddressFrom = "Testveien 1, 7021 Trondheim",
                AddressTo = "Eksempelveien 1, 7022 Trondheim",
                Note = "Example order",
                Services = new OrderServices
                {
                    IsMoving = true,
                    IsPacking = false,
                    IsCleaning = false
                },
                Customer = new Customer
                {
                    Name = "Nils Nilsen",
                    Email = "nils.nilsen@gmail.com",
                    Phone = "44443333"
                }
            });

        orders.Add(new Order
        {
            Id = 2,
            IsDone = false,
            UpdateDate = new DateTime(2022, 06, 03, 12, 0, 0),
            OrderDate = new DateTime(2022, 07, 10, 09, 00, 0),
            AddressFrom = "Testveien 2, 7021 Trondheim",
            AddressTo = "Eksempelveien 2, 7022 Trondheim",
            Note = "Example order",
            Services = new OrderServices
            {
                IsMoving = true,
                IsPacking = true,
                IsCleaning = true
            },
            Customer = new Customer
            {
                Name = "Sara Sarasen",
                Email = "sara.sarasen@gmail.com",
                Phone = "44445555"
            }
        });

        orders.Add(new Order
        {
            Id = 3,
            IsDone = false,
            UpdateDate = new DateTime(2022, 06, 10),
            OrderDate = new DateTime(2022, 07, 10, 09, 00, 0),
            AddressFrom = "Testveien 3, 7021 Trondheim",
            AddressTo = "Eksempelveien 3, 7022 Trondheim",
            Note = "Example order",
            Services = new OrderServices
            {
                IsMoving = true,
                IsPacking = true,
                IsCleaning = true
            },
            Customer = new Customer
            {
                Name = "Jan Jansen",
                Email = "jan.jansen@gmail.com",
                Phone = "44446666"
            }
        });

        return orders;
    }
}