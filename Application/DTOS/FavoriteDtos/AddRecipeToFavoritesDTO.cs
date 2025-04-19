using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.FavoriteDtos
{
    public class AddRecipeToFavoritesDTO
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
    }
}
