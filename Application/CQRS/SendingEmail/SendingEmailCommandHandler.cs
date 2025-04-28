using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SendingEmail
{
    public class SendingEmailCommandHandler : IRequestHandler<SendingEmailCommand>
    {
        private readonly IEmailService _emailService;
        public SendingEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Handle(SendingEmailCommand request, CancellationToken cancellationToken)
        {
            var emailSender =  _emailService.SendEmail(request.to,request.subject,request.body);
        }
    }

}
