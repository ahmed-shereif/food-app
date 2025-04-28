using Application.Helpers;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.UpdateUserEmailVerified
{
    public record UpdateEmailVerifiedCommand(UpdateEmailVerifiedStateViewModel UpdateEmailVerifiedStateViewModel) :IRequest<ResponseViewModel<bool>>;
   
}
