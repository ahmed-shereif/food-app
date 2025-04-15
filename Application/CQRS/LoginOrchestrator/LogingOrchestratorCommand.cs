using Application.CQRS.Token;
using Application.CQRS.Users.Login;
using Application.DTOS.UserDtos;
using Application.Helpers.MappingProfile;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.LoginOrchestrator
{
    public record LogingOrchestratorCommand(LoginUserViewModel LoginOrchestratorViewModel) :IRequest<LoginOrchestratorDto>;

    

}
