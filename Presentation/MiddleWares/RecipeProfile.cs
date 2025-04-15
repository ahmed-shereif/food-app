using Application.DTOS.RecipeDto;
using AutoMapper;
using Presentation.ViewModels.RecipeViewModel;

namespace Presentation.MiddleWares
{
    public class RecipeProfile:Profile
    {
        public RecipeProfile() {
            CreateMap<GetRecipeViewModel,GetRecipeDto>();
            CreateMap<GetAllRecipesViewModel,GetAllRecipesDto>();

        }
    }
}
