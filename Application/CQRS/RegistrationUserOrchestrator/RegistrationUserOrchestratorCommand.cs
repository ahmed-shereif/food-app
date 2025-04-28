using Application.DTOS.UserDtos;
using Application.Helpers;
using Application.ViewModels.UserViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RegistrationUserOrchestrator
{
    public record RegistrationUserOrchestratorCommand(CreateUserViewModel CreateUserViewModel) :IRequest<ResponseViewModel<bool>>;
   
}
