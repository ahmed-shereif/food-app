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
        public AddRecipeToFavoritesHandler(IGeneralRepository<RecipeUserFavorites> generalRepo)
        {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(AddRecipeToFavoritesCommand request, CancellationToken cancellationToken)
        {

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
