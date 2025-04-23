using Application.DTOS.RecipeDto;

namespace Application.DTOS.CategoriesDto
{
    public record GetAllCategoriesDto(int Id, string Name, string Description, ICollection<GetAllRecipesDto> Recipies) : BaseModelDto(Id);

}