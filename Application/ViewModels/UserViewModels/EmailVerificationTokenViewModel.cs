using Azure.Core;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserViewModels
{
    public class EmailVerificationTokenViewModel
    {
        public DateTime CreatedAt { get; set; } 
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddMinutes(1);
                        
        public int UserId { get; set; }
                  
        public Guid Token { get; set; }
        
    }
}
