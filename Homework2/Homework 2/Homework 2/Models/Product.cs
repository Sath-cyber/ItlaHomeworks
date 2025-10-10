namespace Homework_2.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}
