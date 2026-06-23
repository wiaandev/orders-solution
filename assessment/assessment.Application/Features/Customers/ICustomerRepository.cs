using assessment.Domain.Entities;

namespace assessment.Application.Features.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int customerId, CancellationToken ct = default);
}