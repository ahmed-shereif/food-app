using Application.CQRS.Recipes.Commands;
using Application.CQRS.Recipes.Queries;
using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Azure;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.RecipeViewModel;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Add Recipe
        [HttpPost]
        public async Task<ResponseViewModel<bool>> AddRecipe([FromBody] AddRecipeViewModel recipeViewModel)
        {
            var mapedRecipe = recipeViewModel.Map<AddRecipeDto>();
            var result = await _mediator.Send(new AddRecipeCommand(mapedRecipe));

            if (!result.IsSuccess)
                return
                    ResponseViewModel<bool>.Failure(
                        false,
                        result.Message,
                        result.StatusCode);



            return ResponseViewModel<bool>.Success(true, "Recipe added successfully.");
        }

        #endregion

        #region Get Recipe By Id 
    

        [HttpGet("{id}")]
        public async Task<ResponseViewModel<GetRecipeViewModel>> GetRecipeById(int id)
        {

            var result = await _mediator.Send(new GetRecipeByIdQuery(id));
            var mapedRecipe = result.Data.Map<GetRecipeViewModel>();

            if (result.Data == null)
                return ResponseViewModel<GetRecipeViewModel>.Failure(mapedRecipe, "cannot find recipe", ErrorCodeEnum.NotFound);

            return ResponseViewModel<GetRecipeViewModel>.Success(mapedRecipe, "Success");

        }
        #endregion
        #region Get All Recipes

        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<GetAllRecipesViewModel>>> GetAllRecipes()
        {
            var result = await _mediator.Send(new GetAllRecipesQuery());

            if (!result.IsSuccess || result.Data is null)
            {
             
               return   ResponseViewModel<IEnumerable<GetAllRecipesViewModel>>.Failure(null, result.Message, result.StatusCode);
            }

            
    
            var mappedData = result.Data.Map<IEnumerable<GetAllRecipesViewModel>>();
            return ResponseViewModel<IEnumerable<GetAllRecipesViewModel>>.Success(mappedData, "Success");
        }
        #endregion

        #region Delete Recipe
        [HttpDelete]
        public async Task<ResponseViewModel<bool>> DeleteRecipe(int id)
        {
            var result = await _mediator.Send(new RemoveRecipeCommand(id));


            if (result.IsSuccess)
                return ResponseViewModel<bool>.Success(result.Data, result.Message);

            return ResponseViewModel<bool>.Failure(result.Data, result.Message, ErrorCodeEnum.FailerDelete);

        }
        #endregion


    }
}
