using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos.Book;

public record class CreateBookDto(
    [Required][StringLength(50)] string Name,
    string Author,
    int CategoryId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
