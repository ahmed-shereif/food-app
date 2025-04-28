using Application.CQRS.Users.UpdateUserEmailVerified;
using Application.CQRS.Users.VerifyEmail;
using Application.Helpers;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.VerifyEmailOrchestratort
{
    public class VerifyEmailOrchestratortCommandHandler : IRequestHandler<VerifyEmailOrchestratortCommand, ResponseViewModel<bool>>
    {
        private readonly IMediator _mediator;

        public VerifyEmailOrchestratortCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseViewModel<bool>> Handle(VerifyEmailOrchestratortCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new verifyEmailCommand(request.TokenId));
            var updateEmailVerifiedState = new UpdateEmailVerifiedStateViewModel(userId.Data);

        var result =     await _mediator.Send(new UpdateEmailVerifiedCommand(updateEmailVerifiedState));
         
            if (result.IsSuccess)
                return ResponseViewModel<bool>.Success(true, "Email verified successfully.");
         
            
                return ResponseViewModel<bool>.Failure(false, result.Message, result.StatusCode);
          
        }
    }
  
}
