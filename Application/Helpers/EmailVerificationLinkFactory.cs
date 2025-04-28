using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class EmailVerificationLinkFactory (
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator
    )
        
    {
        public string Create(Guid Token)
        {         
            var verificationLink = linkGenerator.GetUriByAction(
                httpContextAccessor.HttpContext!,
                action: "VerifyEmail",
                controller: "User",
                  new {token = Token.ToString() }
            );
            return verificationLink ?? throw new Exception("could not Create email verification link");
        }
        //localhost:5000/api/User/VerifyEmail?token=12345678-1234-1234-1234-123456789012
      

    }
}
