using assessment.Application.Features.Orders;
using assessment.Domain.Entities;

namespace assessment.Application.Features.Customers;

public class CustomerService(IOrderRepository orderRepository) : ICustomerService
{

    public async Task<List<Order>> GetOrders(int customerId, CancellationToken ct)
    {
        return await orderRepository.GetByCustomerIdAsync(customerId, ct);
    }
}