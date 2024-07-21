using BookApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Drama" },
            new { Id = 2, Name = "Horror" },
            new { Id = 3, Name = "Health" },
            new { Id = 4, Name = "Fantasy" },
            new { Id = 5, Name = "Classic" },
            new { Id = 6, Name = "Romance" }
        );
    }

}
