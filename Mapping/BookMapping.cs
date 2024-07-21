using BookApi.Dtos.Book;
using BookApi.Entities;

namespace BookApi;

public static class BookMapping
{
    public static Book ToEntity(this CreateBookDto book)
    {
        return new Book()
        {
            Name = book.Name,
            Author = book.Author,
            CategoryId = book.CategoryId,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate,
        };
    }

    public static Book ToEntity(this UpdateBookDto book, int id)
    {
        return new Book()
        {
            Id = id,
            Name = book.Name,
            Author = book.Author,
            CategoryId = book.CategoryId,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate,
        };
    }

    public static BookSummaryDto ToBookSummaryDto(this Book book)
    {
        return new
        (
            book.Id,
            book.Name,
            book.Author,
            book.Category!.Name,
            book.Price,
            book.ReleaseDate
        );
    }

    public static BookDetailsDto ToBookDetailsDto(this Book book)
    {
        return new(
            book.Id,
            book.Name,
            book.Author,
            book.CategoryId,
            book.Price,
            book.ReleaseDate
        );
    }

}
