using Application.DTOS.RecipeDto;
using AutoMapper;
using Presentation.ViewModels.RecipeViewModel;

namespace Presentation.MappingProfile
{
    public class RecipeProfile:Profile
    {
        public RecipeProfile()
        {
            CreateMap<AddRecipeViewModel, AddRecipeDto>().ReverseMap();
            CreateMap<GetRecipeViewModel, GetRecipeDto>().ReverseMap();
            CreateMap<GetAllRecipesViewModel, GetAllRecipesDto>().ReverseMap();
            CreateMap<GetRecipesByNameOrTagOrCategoryParamsViewModel, GetRecipesByNameOrTagOrCategoryParams>().ReverseMap();
            CreateMap<GetRecipesByNameOrTagOrCategoryDTO, GetRecipesByNameOrTagOrCategoryViewModel>().ReverseMap();

        }
    }
}
