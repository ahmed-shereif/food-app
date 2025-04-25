using Application.DTOS.FavoriteDtos;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.MappingProfile
{
    internal class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap< RecipeUserFavorites, AddRecipeToFavoritesDTO>().ReverseMap();
            CreateMap<RecipeUserFavorites, GetRecipeUserFavoritesDTO>().ReverseMap();
            CreateMap<GetRecipeUserFavoritesDtoRecipeDto, Recipe>().ReverseMap();

        }
    }
}
