using assessment.Application.Features.Customers;
using assessment.Domain.Entities;
using assessment.Infrastructure.Database;

namespace assessment.Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
{
    public async Task<Customer?> GetByIdAsync(int customerId, CancellationToken ct = default)
    {
        return await appDbContext.Customers.FindAsync(customerId, ct);
    }
}