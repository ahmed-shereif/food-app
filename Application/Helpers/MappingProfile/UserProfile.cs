using Application.DTOS.UserDtos;
using Presentation.ViewModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<LoginUserDto,User>().ReverseMap();
            CreateMap<LoginOrchestratorDto, LoginUserViewModel>().ReverseMap();   
        }
    }
  
}
