using Application.DTOS.UserDtos;
using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Registration
{
    public  record RegistrationCommand(CreateUserDto user): IRequest<ResponseViewModel<CreateUserDto>>;

}
