﻿using BookApi.Data;
using BookApi.Dtos.Book;
using BookApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApi;

public static class BooksEndpoints
{
    const string GetBookEndpointName = "GetBook";

    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("books").WithParameterValidation();

        //GET /books
        group.MapGet("/", async (BookStoreContext dbContext) => await dbContext.Books.Include(book => book.Category).Select(book => book.ToBookDetailsDto()).AsNoTracking().ToListAsync());

        //GET /books/id
        group.MapGet("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            Book? book = await dbContext.Books.FindAsync(id);
            return book == null ? Results.NotFound() : Results.Ok(book.ToBookDetailsDto());
        }).WithName(GetBookEndpointName);

        //POST
        group.MapPost("/", async (CreateBookDto newBook, BookStoreContext dbContext) =>
        {
            Book book = newBook.ToEntity();
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetBookEndpointName, new { id = book.Id }, book.ToBookDetailsDto());
        });

        //PUT /books/id
        group.MapPut("/{id}", async (int id, UpdateBookDto updateBook, BookStoreContext dbContext) =>
        {
            var existingBook = await dbContext.Books.FindAsync(id);

            if (existingBook == null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingBook).CurrentValues.SetValues(updateBook.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();

        });

        //DELETE /books/id
        group.MapDelete("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
