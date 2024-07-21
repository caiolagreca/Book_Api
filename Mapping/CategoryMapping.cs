using BookApi.Dtos.Book;
using BookApi.Dtos.Category;

namespace BookApi;

public static class CategoryMapping
{
    public static Category ToEntity(this CreateCategoryDto category)
    {
        return new Category()
        {
            Name = category.Name,
        };
    }

    public static Category ToEntity(this UpdateCategoryDto category, int id)
    {
        return new Category()
        {
            Id = id,
            Name = category.Name,
        };
    }

    public static CategoryDetailsDto ToCategoryDetailsDto(this Category category)
    {
        return new(
            category.Id,
            category.Name
        );
    }

}
