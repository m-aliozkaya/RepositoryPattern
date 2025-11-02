using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Web.Models;

namespace RepositoryPattern.Web.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}
