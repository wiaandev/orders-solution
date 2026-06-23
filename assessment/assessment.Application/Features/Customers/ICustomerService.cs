using assessment.Domain.Entities;

namespace assessment.Application.Features.Customers;

public interface ICustomerService
{
    Task<List<Order>> GetOrders(int customerId, CancellationToken ct = default);
}