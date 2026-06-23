using assessment.Application.Features.Products;
using assessment.Domain.Entities;
using assessment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(int productId, CancellationToken ct)
    {
       return await appDbContext.Products.FirstOrDefaultAsync(u => u.Id == productId, ct);
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken ct)
    {
        return await appDbContext.Products.AsNoTracking().ToListAsync(ct);
    }
    
    public async Task<List<Product>> GetProductsByIdAsync(List<int> ids, CancellationToken ct)
    {
        return await appDbContext.Products.Where(u => ids.Contains(u.Id)).ToListAsync(ct);
    }
}