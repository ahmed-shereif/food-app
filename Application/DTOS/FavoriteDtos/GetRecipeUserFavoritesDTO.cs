using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOS.FavoriteDtos
{
    public class GetRecipeUserFavoritesDTO
    {
        public GetRecipeUserFavoritesDtoRecipeDto Recipe { get; set; }
    }
}