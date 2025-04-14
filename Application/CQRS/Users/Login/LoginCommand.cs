using Application.DTOS.UserDtos;
using Application.ViewModels;
using MediatR;
using Presentation.ViewModels.ResponseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Login
{
    public record LoginCommand(LoginUserViewModel LoginUser) : IRequest<ResponseViewModel<LoginUserDto>>;
   

}
