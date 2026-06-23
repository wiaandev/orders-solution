using assessment.Domain.Entities;

namespace assessment.Application.Features.Products;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int productId, CancellationToken ct);

    Task<List<Product>> GetProductsAsync(CancellationToken ct);

    Task<List<Product>> GetProductsByIdAsync(List<int> ids, CancellationToken ct);
}