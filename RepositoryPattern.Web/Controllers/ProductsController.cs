using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Web.Models;
using RepositoryPattern.Web.Services;

namespace RepositoryPattern.Web.Controllers;

public class ProductsController(ProductService productService) : Controller
{
    private readonly ProductService _productService = productService;

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        const int lowStockThreshold = 5;
        var products = await _productService.GetProductsAsync(cancellationToken);
        var lowStock = await _productService.GetLowStockAsync(lowStockThreshold, cancellationToken);

        var viewModel = new ProductListViewModel(products, lowStock, lowStockThreshold);
        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult Create() => View(new Product());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        await _productService.CreateAsync(product, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, CancellationToken cancellationToken)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(product);
        }

        await _productService.UpdateAsync(product, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
