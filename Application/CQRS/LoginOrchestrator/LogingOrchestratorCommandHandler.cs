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
    public class LogingOrchestratorCommandHandler : IRequestHandler<LogingOrchestratorCommand, LoginOrchestratorDto>
    {
        private readonly IMediator _mediator;

        public LogingOrchestratorCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LoginOrchestratorDto> Handle(LogingOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var MappingtoLoginViewModel = request.LoginOrchestratorViewModel.Map<LoginUserViewModel>();
            var User =  await _mediator.Send(new LoginCommand(MappingtoLoginViewModel), cancellationToken);

           var mappingToUser = User.Data.Map<User>();
            var token = await _mediator.Send(new TokenGenerationCommand(mappingToUser), cancellationToken);
            return new LoginOrchestratorDto
            {
                Token = token,
                UserName = mappingToUser.UserName,
                Email = mappingToUser.Email,
            };
        }
    }
}
