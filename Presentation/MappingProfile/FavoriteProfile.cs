using Application.DTOS.FavoriteDtos;
using AutoMapper;
using Presentation.ViewModels.RecipeFavoritesViewModel;

namespace Presentation.MappingProfile
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<AddRecipeToFavoritesDTO, AddRecipeToFavoritesViewModel>().ReverseMap();
            CreateMap<GetRecipeUserFavoritesDTO, GetRecipeUserFavoritesViewModel>().ReverseMap();
            CreateMap<GetRecipeUserFavoritesDtoRecipeDto, GetRecipeUserFavoritesViewModelRecipeViewModel>().ReverseMap();
        }
    }
}
