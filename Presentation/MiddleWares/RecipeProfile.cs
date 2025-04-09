using Application.DTOS.RecipeDto;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
