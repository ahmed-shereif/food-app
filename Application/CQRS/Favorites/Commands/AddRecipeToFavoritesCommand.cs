using Application.Helpers;
using Domain.Repositories;
using Domain.Models;



using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOS.FavoriteDtos;
using Application.Helpers.MappingProfile;
using Domain.Enums;

namespace Application.CQRS.Favorites.Commands
{
    public record AddRecipeToFavoritesCommand(AddRecipeToFavoritesDTO AddRecipeToFavoritesDTO) : IRequest<ResponseViewModel<bool>>;
    public class AddRecipeToFavoritesHandler : IRequestHandler<AddRecipeToFavoritesCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<RecipeUserFavorites> _generalRepo;
        private readonly IGeneralRepository<Recipe> _recipeRepo;
        private readonly IGeneralRepository<User> _userRepo;

        public AddRecipeToFavoritesHandler(IGeneralRepository<RecipeUserFavorites> generalRepo, IGeneralRepository<Recipe> recipeRepo, IGeneralRepository<User> userRepo)
        {
            _generalRepo = generalRepo;
            _recipeRepo = recipeRepo;
            _userRepo = userRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(AddRecipeToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var userExist = _userRepo.Get(x => x.Id == request.AddRecipeToFavoritesDTO.UserId).FirstOrDefault();
            var recipeExist = _recipeRepo.Get(x => x.Id == request.AddRecipeToFavoritesDTO.RecipeId).FirstOrDefault();
            if (userExist == null)
            {
                return ResponseViewModel<bool>.Failure(
                    false,
                    $"User not found.",
                    ErrorCodeEnum.NotFound
                );
            }
            if (recipeExist == null)
            {
                return ResponseViewModel<bool>.Failure(
                    false,
                    $"Recipe not found.",
                    ErrorCodeEnum.NotFound
                );
            }
            RecipeUserFavorites recipeUserFavorites = request.AddRecipeToFavoritesDTO.Map<RecipeUserFavorites>();
            RecipeUserFavorites isAdded = await _generalRepo.AddAsync(recipeUserFavorites);
            if (isAdded != null)
            {
                var result = await _generalRepo.SaveChangesAsync();
                if (result > 0)
                {
                    return ResponseViewModel<bool>.Success(true, "Recipe added to favorites successfully.");
                }
            }
            return ResponseViewModel<bool>.Failure(
                false,
                $"An error occurred while adding the recipe to favorites: ",
                ErrorCodeEnum.ServerError
            );


        }
    }
}
