using Application.CQRS.EmailVerificationLinkFactoryy;
using Application.CQRS.SendingEmail;
using Application.CQRS.Users.CreateEmailVerificationToken;
using Application.CQRS.Users.Registration;
using Application.DTOS.UserDtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Application.ViewModels.UserViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RegistrationUserOrchestrator
{
    public class RegistrationUserOrchestratorCommandHandler : IRequestHandler<RegistrationUserOrchestratorCommand , ResponseViewModel<bool>>
    {
        private readonly IMediator _mediator;
        public RegistrationUserOrchestratorCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResponseViewModel<bool>> Handle(RegistrationUserOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var mappingToDto = request.CreateUserViewModel.Map<CreateUserDto>();
            var registrationUser =await _mediator.Send(new RegistrationCommand(mappingToDto),cancellationToken);

            var mappingToEmailVerificationTokenViewModel = registrationUser.Data.Map<EmailVerificationTokenViewModel>();
            var CreateEmailVerificationToken =await _mediator.Send(new CreateEmailVerificationTokenCommand(mappingToEmailVerificationTokenViewModel), cancellationToken);

            var emailVerificationLink = await _mediator.Send(new EmailVerificationLinkFactoryCommand(CreateEmailVerificationToken.Data.Token), cancellationToken);
            await _mediator.Send(new SendingEmailCommand(
                mappingToDto.Email,
                "Email Verification",
                $"Hello {mappingToDto.UserName}, please verify your email by clicking the link below.\n" +
                $"<a href='{emailVerificationLink}'>Click here to verify your email</a>"),
                cancellationToken);

            return ResponseViewModel<bool>.Success(true, "User registered successfully. Please check your email for verification.");
        }
 
    }
}
