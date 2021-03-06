using System.ComponentModel.DataAnnotations;

namespace MovingCompanyAPI.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public bool IsDone { get; set; }

    /// <summary>Date when order was last updated</summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>Date when order should be performed</summary>
    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string AddressFrom { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string AddressTo { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    [Required]
    public OrderServices Services { get; set; } = new OrderServices();

    [Required]
    public Customer Customer { get; set; } = new Customer();
}

public class OrderServices
{
    public bool IsMoving { get; set; }
    public bool IsPacking { get; set; }
    public bool IsCleaning { get; set; }
}

public class Customer
{
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}