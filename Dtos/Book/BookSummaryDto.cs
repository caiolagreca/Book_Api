namespace BookApi.Dtos.Book;

public record class BookSummaryDto(int Id, string Name, string Author, string Category, decimal Price, DateOnly ReleaseDate);

