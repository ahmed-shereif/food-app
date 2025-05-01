using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmailVerificationToken : BaseModel
    {
        public Guid Token { get; set; } 
        
        public DateTime ExpirationDate { get; set; } 

        public int UserId { get; set; }
        public virtual User User { get; set; }
  
    }
}
