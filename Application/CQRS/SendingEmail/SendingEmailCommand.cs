using Application.ViewModels.EmailViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SendingEmail
{
    public record SendingEmailCommand(string to, string subject, string body) :IRequest;
  
}
