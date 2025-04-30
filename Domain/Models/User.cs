using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? OTPSecretKey { get; set; }

        public bool EmailVerified { get; set; } = false;
        public Role Role { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

    }
}
