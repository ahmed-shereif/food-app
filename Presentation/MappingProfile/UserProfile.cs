using Application.DTOS.UserDtos;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Presentation.ViewModels.UserViewModels;

namespace Presentation.MappingProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, CreateUserDto>().ReverseMap();
            CreateMap<LoginOrchestratorViewModel, LoginUserViewModel>().ReverseMap();
            CreateMap<LoginOrchestratorDto, LoginOrchestratorViewModel>().ReverseMap();
            CreateMap<RegistratioUserOrchestratorViewModel, CreateUserViewModel>().ReverseMap();


        }
    }
}
