using assessment.Domain.Entities;

namespace assessment.Application.Features.Orders;

public interface IOrderRepository
{
    Task<Order?> GetBydIdAsync(int orderId, CancellationToken ct = default);
    
    Task<List<Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
}