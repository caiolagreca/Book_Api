namespace BookApi.Dtos.Book;

public record class BookDetailsDto(int Id, string Name, string Author, int CategoryId, decimal Price, DateOnly ReleaseDate);
