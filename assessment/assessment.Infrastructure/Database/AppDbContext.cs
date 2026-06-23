using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }
}