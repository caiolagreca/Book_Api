using BookApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApi;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
