using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.RecipeDto
{
   public record UpdateRecipeDto(int id, string Name, string ImageUrl, decimal Price, string Description, string Tag, string Category);
    
}
