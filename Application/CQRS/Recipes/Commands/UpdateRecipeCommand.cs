using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Commands
{
   public record UpdateRecipeCommand(UpdateRecipeDto recipeDto):IRequest<ResponseViewModel<bool>>;

    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<Recipe> _generalRepository;

        public UpdateRecipeCommandHandler(IGeneralRepository<Recipe> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<ResponseViewModel<bool>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _generalRepository.GetByIdAsync(request.recipeDto.id);
            if (existingRecipe == null || existingRecipe.IsDeleted)
            {
                return ResponseViewModel<bool>.Failure(false, "Recipe not found", ErrorCodeEnum.NotFound);
            }
           // Map Dto to recipe
            var updatedRecipe = request.recipeDto.Map<Recipe>();
           

            _generalRepository.UpdateInclude(updatedRecipe,
           nameof(Recipe.Name),
           nameof(Recipe.ImageUrl),
           nameof(Recipe.Price),
           nameof(Recipe.Description),
           nameof(Recipe.Tag)
           //nameof(Recipe.Category) 
       );
            await _generalRepository.SaveChangesAsync();

            return ResponseViewModel<bool>.Success(true, "Recipe updated successfully");


        }
    }
}
