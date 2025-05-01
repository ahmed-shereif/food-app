using Azure;
using Domain.Contracts;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
      
        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        //public async Task SendEmail(SendEmailViewModel emailViewModel)
        //{
        //  await  _fluentEmail
        //        .To(emailViewModel.to)
        //        .Subject(emailViewModel.subject)
        //        .Body(emailViewModel.body , isHtml: false)
        //        .SendAsync();



        public async Task SendEmail(string to, string subject, string body)
        {
 
          var email = await _fluentEmail
         .To("shehabmohamed6n789@gmail.com")
         .Subject(subject)
         .Body(body, true)
         .SendAsync();
        } 
    
    }
}
