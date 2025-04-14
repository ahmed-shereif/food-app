using Application.DTOS.UserDtos;
using MediatR;
using Presentation.ViewModels.ResponseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Registration
{
    public  record RegistrationCommand(CreateUserDto user): IRequest<ResponseViewModel<CreateUserDto>>;

}
