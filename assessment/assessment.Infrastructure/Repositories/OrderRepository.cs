using assessment.Application.Features.Orders;
using assessment.Domain.Entities;
using assessment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infrastructure.Repositories;

public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
{
    public async Task<Order?> GetBydIdAsync(int orderId, CancellationToken ct = default)
    {
        return await appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, ct);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct)
    {
        return await appDbContext.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(ct);
    }
}