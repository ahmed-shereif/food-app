using Application.DTOS.RecipeDto;
using Application.DTOS;

namespace Presentation.ViewModels.CategoriesViewModel
{

    public record GetAllCategroiesViewModel(int Id, string Name, string Description, ICollection<GetAllRecipesDto> Recipies) : BaseModelDto(Id);

}