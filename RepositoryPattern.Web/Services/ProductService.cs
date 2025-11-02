using RepositoryPattern.Web.Models;
using RepositoryPattern.Web.Repositories;

namespace RepositoryPattern.Web.Services;

public class ProductService(IProductRepository productRepository)
{
    private readonly IProductRepository _productRepository = productRepository;

    public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default) =>
        _productRepository.GetAllAsync(cancellationToken);

    public Task<List<Product>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default) =>
        _productRepository.GetLowStockProductsAsync(threshold, cancellationToken);

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _productRepository.GetByIdAsync(id, cancellationToken);

    public Task CreateAsync(Product product, CancellationToken cancellationToken = default) =>
        _productRepository.AddAsync(product, cancellationToken);

    public Task UpdateAsync(Product product, CancellationToken cancellationToken = default) =>
        _productRepository.UpdateAsync(product, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _productRepository.DeleteAsync(id, cancellationToken);
}
