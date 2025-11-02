using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Web.Data;
using RepositoryPattern.Web.Models;
using RepositoryPattern.Web.Repositories;
using RepositoryPattern.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

await SeedDataAsync(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapFallbackToController("Index", "Products");

app.Run();

static async Task SeedDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.EnsureCreatedAsync();

    if (context.Products.Any())
    {
        return;
    }

    context.Products.AddRange(
        new Product { Name = "Mechanical Keyboard", Price = 149.99m, Stock = 12, Description = "Hot-swappable switches, RGB lighting." },
        new Product { Name = "Noise Cancelling Headphones", Price = 249.50m, Stock = 6, Description = "Wireless over-ear headphones with ANC." },
        new Product { Name = "Ergonomic Mouse", Price = 59.90m, Stock = 18, Description = "Vertical mouse for better wrist posture." }
    );

    await context.SaveChangesAsync();
}
