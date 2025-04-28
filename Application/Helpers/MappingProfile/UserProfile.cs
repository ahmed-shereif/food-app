using Application.DTOS.UserDtos;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
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
            CreateMap<CreateUserViewModel, CreateUserDto>().ReverseMap();   
            CreateMap<CreateUserDto,User>().ReverseMap().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)); ;   
             CreateMap<CreateUserDto, EmailVerificationTokenViewModel>().ReverseMap();
             CreateMap<EmailVerificationToken, EmailVerificationTokenViewModel>().ReverseMap();
             CreateMap<UpdateEmailVerifiedStateViewModel, User>().ReverseMap();

            CreateMap<EmailVerificationToken, EmailVerificationTokenDto>()
                 .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                 .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                 .ReverseMap();
        }
    }
  
}
