using Application.ViewModels.UserViewModels;
using Domain.Enums;

namespace Presentation.ViewModels.UserViewModels
{
    public record RegistratioUserOrchestratorViewModel(string UserName , string Password, string phone,string Email, Role Role );
   
  
}
