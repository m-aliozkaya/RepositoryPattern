using System.Linq;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Web.Data;
using RepositoryPattern.Web.Models;

namespace RepositoryPattern.Web.Repositories;

public class ProductRepository(AppDbContext context) : EfRepository<Product>(context), IProductRepository
{
    public Task<List<Product>> GetLowStockProductsAsync(int threshold, CancellationToken cancellationToken = default) =>
        Context.Products
            .AsNoTracking()
            .Where(product => product.Stock <= threshold)
            .OrderBy(product => product.Stock)
            .ToListAsync(cancellationToken);
}
