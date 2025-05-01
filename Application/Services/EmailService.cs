using Application.ViewModels.EmailViewModel;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Domain.Contracts;

namespace Application.Services
{
  public  class EmailService : IEmailService
    {
       private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            
                var response = await _fluentEmail
                    .To(to)
                    .Subject(subject)
                    .Body(body, isHtml: true)
                    .SendAsync();

           
           
        }
    }
}
