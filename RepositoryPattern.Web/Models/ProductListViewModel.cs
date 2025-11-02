using System.Collections.Generic;

namespace RepositoryPattern.Web.Models;

public record ProductListViewModel(IReadOnlyCollection<Product> Products, IReadOnlyCollection<Product> LowStockProducts, int LowStockThreshold);
