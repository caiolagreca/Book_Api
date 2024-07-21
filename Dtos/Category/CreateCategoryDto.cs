using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos.Category;

public record class CreateCategoryDto([Required] string Name);
