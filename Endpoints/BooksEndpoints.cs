using System.Security.Claims;
using BookApi.Data;
using BookApi.Dtos.Book;
using BookApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApi;

public static class BooksEndpoints
{
    public static Dictionary<string, List<string>> booksMap =
        new()
        {
            {
                "player1",
                new List<string>() { "st fight 1", "fifa 15" }
            },
            {
                "player2",
                new List<string>() { "st fight 2", "fifa 24", "minecraft", "skyrim" }
            }
        };

    public static Dictionary<string, List<string>> subscriptionBooks =
        new()
        {
            {
                "silver",
                new List<string>() { "st fight 1", "fifa 15" }
            },
            {
                "gold",
                new List<string>()
                {
                    "st fight 1",
                    "fifa 15",
                    "st fight 2",
                    "fifa 24",
                    "minecraft",
                    "skyrim"
                }
            }
        };
    const string GetBookEndpointName = "GetBook";

    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("books").WithParameterValidation();

        //GET /books
        group
            .MapGet(
                "/",
                async (BookStoreContext dbContext) =>
                    await dbContext
                        .Books.Include(book => book.Category)
                        .Select(book => book.ToBookDetailsDto())
                        .AsNoTracking()
                        .ToListAsync()
            )
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("admin");
            }); //RequireAuthorization: Adiciona um requisito de autorização para o endpoint. Somente usuários autenticados poderão acessar este endpoint.
        //RequireRole("admin"): Especifica que apenas usuários com a função "admin" podem acessar este endpoint.

        //GET /books/id
        group
            .MapGet(
                "/{id}",
                async (int id, BookStoreContext dbContext) =>
                {
                    Book? book = await dbContext.Books.FindAsync(id);
                    return book == null ? Results.NotFound() : Results.Ok(book.ToBookDetailsDto());
                }
            )
            .WithName(GetBookEndpointName);

        //POST
        group.MapPost(
            "/",
            async (CreateBookDto newBook, BookStoreContext dbContext) =>
            {
                Book book = newBook.ToEntity();
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetBookEndpointName,
                    new { id = book.Id },
                    book.ToBookDetailsDto()
                );
            }
        );

        //PUT /books/id
        group.MapPut(
            "/{id}",
            async (int id, UpdateBookDto updateBook, BookStoreContext dbContext) =>
            {
                var existingBook = await dbContext.Books.FindAsync(id);

                if (existingBook == null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingBook).CurrentValues.SetValues(updateBook.ToEntity(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        //DELETE /books/id
        group.MapDelete(
            "/{id}",
            async (int id, BookStoreContext dbContext) =>
            {
                await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            }
        );

        //GET /mygame
        //Dependecy injection (ClaimsPrincipal user) - Obtém as claims do usuário autenticado, fornecendo detalhes como o nome de usuário.
        group
            .MapGet(
                "/mybooks",
                (ClaimsPrincipal user) =>
                {
                    var hasClaim = user.HasClaim(claim => claim.Type == "subscription");

                    if (hasClaim)
                    {
                        var subs =
                            user.FindFirstValue("subscription")
                            ?? throw new Exception("Claim has no value");
                        foreach (var claim in user.Claims)
                        {
                            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                        }
                        return Results.Ok(subscriptionBooks[subs]);
                    }

                    //Garante que o nome de usuário não é nulo. Se for, lança uma exceção.
                    ArgumentNullException.ThrowIfNull(user.Identity?.Name);
                    var username = user.Identity.Name;

                    if (!booksMap.ContainsKey(username))
                    {
                        return Results.Empty;
                    }

                    //LOG apenas para visualizar as claims (name user, role, email, etc):

                    return Results.Ok(booksMap[username]);
                }
            )
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("player");
            });

        return group;
    }
}
