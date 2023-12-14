using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Test.Data;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class AppDBContext : IdentityDbContext<IdentityUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
}

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int? CategoryId { get; set; }
    public ICollection<OrderProduct>? OrderProducts { get; set; }
}

public class Order
{
    public int? Id { get; set; }
    [StringLength(450)]// chiều dài bằng userid trong bảng user dùng cho identity
    public string UserId { get; set; } = null!;
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<OrderProduct>? OrderProducts { get; set; }
}

public class Category
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OrderProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int OrderId { get; set; }
    public Order? Orders { get; set; }

    public int? Quantity { get; set; }
    double? Price { get; set; }
}
