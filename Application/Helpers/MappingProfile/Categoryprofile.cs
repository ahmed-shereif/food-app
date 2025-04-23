using Application.DTOS.CategoriesDto;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Domain.Models;
using Application.DTOS.CategoriesDto;

namespace Application.Helpers.MappingProfile
{
    public class Categoryprofile : Profile
    {
        public Categoryprofile()
        {
            CreateMap<Category, GetAllCategoriesDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description))
                .ForCtorParam("Recipies", opt => opt.MapFrom(src => src.Recipes));
        }
    }
}
