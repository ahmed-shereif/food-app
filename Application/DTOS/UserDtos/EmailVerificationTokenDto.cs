using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.UserDtos
{
    public class EmailVerificationTokenDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationDate { get; set; } 
        public int UserId { get; set; }
        public Guid Token { get; set; } 
    }
}
