namespace BookApi.Entities;

public class Book
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
