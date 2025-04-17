using Application.CQRS.Favorites.Commands;
using Application.CQRS.Favorites.Queries;
using Application.CQRS.Recipes.Queries;
using Application.DTOS.FavoriteDtos;
using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Azure.Core;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.RecipeFavoritesViewModel;
using Presentation.ViewModels.RecipeViewModel;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FavoriteController : BaseController
    {
        private readonly IMediator _mediator;
        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public ResponseViewModel<bool> AddRecipeToFavorites(AddRecipeToFavoritesViewModel addRecipeToFavoritesViewModel)
        {

            return _mediator.Send(new AddRecipeToFavoritesCommand(addRecipeToFavoritesViewModel.Map<AddRecipeToFavoritesDTO>())).Result;
        }
        [HttpGet()]
        public ResponseViewModel<IEnumerable<GetRecipeUserFavoritesViewModel>> GetFavoriteRecipesByUserId(int userId)
        {
            var recipes = _mediator.Send(new GetFavoriteRecipesByUserIdQuery(userId)).Result.Data;
            IEnumerable<GetRecipeUserFavoritesViewModel> mappedRecipes = recipes.AsQueryable().Project<GetRecipeUserFavoritesViewModel>();

            if (mappedRecipes is null)
            {
                return ResponseViewModel<IEnumerable<GetRecipeUserFavoritesViewModel>>.Failure(null, "Recipes not found", ErrorCodeEnum.NotFound);
            }

            return ResponseViewModel<IEnumerable<GetRecipeUserFavoritesViewModel>>.Success(mappedRecipes, "Success");
        }
    }
    
}
