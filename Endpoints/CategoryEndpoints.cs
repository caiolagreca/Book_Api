using BookApi.Data;
using BookApi.Dtos.Category;
using Microsoft.EntityFrameworkCore;

namespace BookApi;

public static class CategoryEndpoints
{
    const string GetCategoryEndpointName = "GetCategory";

    public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("categories").WithParameterValidation();

        //GET /categories
        group.MapGet("/", async (BookStoreContext dbContext) => await dbContext.Categories.Select(category => category.ToCategoryDetailsDto()).AsNoTracking().ToListAsync());

        //GET /categories/id
        group.MapGet("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            Category? category = await dbContext.Categories.FindAsync(id);
            return category == null ? Results.NotFound() : Results.Ok(category.ToCategoryDetailsDto());
        }).WithName(GetCategoryEndpointName);

        //POST
        group.MapPost("/", async (CreateCategoryDto newCategory, BookStoreContext dbContext) =>
        {
            Category category = newCategory.ToEntity();
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetCategoryEndpointName, new { id = category.Id }, category.ToCategoryDetailsDto());
        });

        //PUT /categories/id
        group.MapPut("/{id}", async (int id, UpdateCategoryDto updateCategory, BookStoreContext dbContext) =>
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingCategory).CurrentValues.SetValues(updateCategory.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //DELETE /categories/id
        group.MapDelete("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            await dbContext.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
