using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Favorites.Commands
{
    public record RemoveRecipeFromFavoritesCommand(int userId, int recipeId) : IRequest<ResponseViewModel<bool>>;
    public class RemoveRecipeFromFavoritesCommandHandler : IRequestHandler<RemoveRecipeFromFavoritesCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<RecipeUserFavorites> _generalRepo;
        public RemoveRecipeFromFavoritesCommandHandler(IGeneralRepository<RecipeUserFavorites> generalRepo)
        {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(RemoveRecipeFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            
            var recipeUserFavorites = await _generalRepo.Get(x => x.UserId == request.userId && x.RecipeId == request.recipeId ).FirstOrDefaultAsync();
            if (recipeUserFavorites == null)
            {
                return ResponseViewModel<bool>.Failure(
                    false,
                    $"Recipe not found in favorites.",
                    ErrorCodeEnum.NotFound
                );
            }
            await _generalRepo.Delete(recipeUserFavorites.Id);
            var result = await _generalRepo.SaveChangesAsync();
            if (result > 0)
            {
                return ResponseViewModel<bool>.Success(true, "Recipe removed from favorites successfully.");
            }
            return ResponseViewModel<bool>.Failure(
                false,
                $"An error occurred while removing the recipe from favorites: ",
                ErrorCodeEnum.ServerError
            );
        }
    }


}
