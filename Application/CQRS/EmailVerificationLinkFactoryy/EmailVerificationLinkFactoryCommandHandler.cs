using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.EmailVerificationLinkFactoryy
{
    public class EmailVerificationLinkFactoryCommandHandler : IRequestHandler<EmailVerificationLinkFactoryCommand,string>
    {
        private readonly EmailVerificationLinkFactory _emailVerificationLinkFactory;

        public EmailVerificationLinkFactoryCommandHandler(EmailVerificationLinkFactory emailVerificationLinkFactory)
        {
            _emailVerificationLinkFactory = emailVerificationLinkFactory;
        }

        public async Task<string> Handle(EmailVerificationLinkFactoryCommand request, CancellationToken cancellationToken)
        {
            var emailVerificationLink = _emailVerificationLinkFactory.Create(request.Token);
            return emailVerificationLink;
        }

    }
}
