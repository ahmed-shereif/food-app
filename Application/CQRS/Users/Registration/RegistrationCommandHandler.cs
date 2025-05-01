using Application.DTOS.UserDtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand , ResponseViewModel<CreateUserDto>>
    {
        private readonly IGeneralRepository<User> _generalRepository;

        public RegistrationCommandHandler(IGeneralRepository<User> generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<ResponseViewModel<CreateUserDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = await _generalRepository.Get(x => x.Email == request.user.Email).FirstOrDefaultAsync();

            if (user != null)
            {
               return ResponseViewModel<CreateUserDto>.Failure(null, "ssss", ErrorCodeEnum.AlreadyExist);
            };
            //   var hashingPassword = new passwordHasher<User>.PasswordHasher();
            var mapp = request.user;
            var MappingToUser = mapp.Map<User>();
            MappingToUser.Password = BCrypt.Net.BCrypt.HashPassword(request.user.Password);

            var CreateNewUser = await _generalRepository.AddAsync(MappingToUser);
            var result = await _generalRepository.SaveChangesAsync();
            var mappingToCreateUserDto = CreateNewUser.Map<CreateUserDto>();
            if (result > 0)
            {
              return  ResponseViewModel<CreateUserDto>.Success(mappingToCreateUserDto, "sasasasa");
            }
            else
            {
             return   ResponseViewModel<CreateUserDto>.Failure(null, "ssss", ErrorCodeEnum.BadRequest);
            }
        }
    }
  
}
