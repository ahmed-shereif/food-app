using Application.DTOS.UserDtos;
using Application.Helpers.MappingProfile;
using Domain.Contracts;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Presentation.ViewModels.ResponseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseViewModel<LoginUserDto>>
    {
        private readonly IGeneralRepository<User> _generalRepository;
      //  private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IGeneralRepository<User> customerRepo, IJwtProvider jwtProvider)
        {
            _generalRepository = customerRepo;
           // _jwtProvider = jwtProvider;
        }

        public async Task<ResponseViewModel<LoginUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           // var mappingToLoginUserDto = request.LoginUser.Map<LoginUserDto>();
            var user =  _generalRepository.Get(x => x.Email == request.LoginUser.Email).FirstOrDefault();

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.LoginUser.Password, user.Password))
            {
                return ResponseViewModel<LoginUserDto>.Failure(null, "ssss", ErrorCodeEnum.FailerDeleteRoom);

            }
       //    string token =  _jwtProvider.GenerateToken(user);

            return ResponseViewModel<LoginUserDto>.Success(new LoginUserDto
            {
              Id = user.Id,
              Role = user.Role,
              UserName = user.UserName,
            }, "Login successfully");


        }

    }
}
