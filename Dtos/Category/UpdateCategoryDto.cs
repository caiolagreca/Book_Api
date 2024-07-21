using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos.Category;

public record class UpdateCategoryDto([Required] string Name);