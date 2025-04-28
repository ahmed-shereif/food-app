using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public record UpdateEmailVerifiedStateViewModel(int id, bool EmailVerified = true);

}
