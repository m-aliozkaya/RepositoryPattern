using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.Web.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    public int Stock { get; set; }
}
