using assessment.Domain.Entities;
using assessment.Domain.Enum;
using assessment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using RefactoringExercise.Enum;

namespace assessment.Infrastructure.Bootstrap;

public class SeedService(AppDbContext appDbContext)
{
    public async Task SeedAsync(CancellationToken ct = default)
    {
        await SeedCustomers(ct);
        await SeedProducts(ct);
        await SeedOrders(ct);
    }

    private async Task SeedCustomers(CancellationToken ct)
    {
        if (await appDbContext.Customers.AnyAsync(ct))
            return;

        var customers = new List<Customer>
        {
            new() { Name = "John",  Surname = "Doe",   Email = "john.doe@example.com" },
            new() { Name = "Jane",  Surname = "Smith",  Email = "jane.smith@example.com" }
        };

        appDbContext.Customers.AddRange(customers);
        await appDbContext.SaveChangesAsync(ct);
    }

    private async Task SeedProducts(CancellationToken ct)
    {
        if (await appDbContext.Products.AnyAsync(ct))
            return;

        var products = new List<Product>
        {
            new() { Name = "Product 1", Price = 200 },
            new() { Name = "Product 2", Price = 300 }
        };

        appDbContext.Products.AddRange(products);
        await appDbContext.SaveChangesAsync(ct);
    }

    private async Task SeedOrders(CancellationToken ct)
    {
        if (await appDbContext.Orders.AnyAsync(ct))
            return;

        var customer1 = await appDbContext.Customers.FirstAsync(c => c.Email == "john.doe@example.com", ct);
        var customer2 = await appDbContext.Customers.FirstAsync(c => c.Email == "jane.smith@example.com", ct);

        var orders = new List<Order>
        {
            new()
            {
                CustomerId    = customer1.Id,
                Total         = 500,
                Status        = OrderStatus.Pending,
                PaymentMethod = PayMethod.CreditCard
            },
            new()
            {
                CustomerId    = customer2.Id,
                Total         = 200,
                Status        = OrderStatus.Delivered,
                PaymentMethod = PayMethod.Paypal
            }
        };

        appDbContext.Orders.AddRange(orders);
        await appDbContext.SaveChangesAsync(ct);
    }
}