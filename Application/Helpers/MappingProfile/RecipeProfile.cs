using Application.DTOS.RecipeDto;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.MappingProfile
{
   public class RecipeProfile:Profile
    {
        public RecipeProfile() {
            CreateMap<Recipe,GetRecipeDto>().ReverseMap();
            CreateMap<Recipe,GetAllRecipesDto>().ReverseMap();
            CreateMap<Recipe,AddRecipeDto>().ReverseMap();
            CreateMap<Recipe, GetRecipesByNameOrTagOrCategoryDTO>().ReverseMap();



        }
    }
}
