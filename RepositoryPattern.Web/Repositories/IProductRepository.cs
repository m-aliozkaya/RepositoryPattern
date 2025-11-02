using RepositoryPattern.Web.Models;

namespace RepositoryPattern.Web.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetLowStockProductsAsync(int threshold, CancellationToken cancellationToken = default);
}
