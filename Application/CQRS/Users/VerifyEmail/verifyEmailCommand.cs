using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.VerifyEmail
{
    public record verifyEmailCommand(Guid TokenId):IRequest<ResponseViewModel<int>>;
  
}
