using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.EmailVerificationLinkFactoryy
{
    public record EmailVerificationLinkFactoryCommand(Guid Token) : IRequest<string>;

}