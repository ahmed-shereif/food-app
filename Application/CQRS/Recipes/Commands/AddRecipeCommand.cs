using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Commands
{
    public record AddRecipeCommand(AddRecipeDto recipeDto):IRequest<ResponseViewModel<bool>>;
    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, ResponseViewModel<bool>>
    {
        private readonly GeneralRepository<Recipe> _generalRepo;

        public AddRecipeCommandHandler(GeneralRepository<Recipe> generalRepo)
        {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
           
            var recipe = request.recipeDto.Map<Recipe>();
               
               await _generalRepo.AddAsync(recipe);
               var result =  await _generalRepo.SaveChangesAsync();
                if (result > 0)


                return ResponseViewModel<bool>.Success(true, "Recipe added successfully.");

            
                return ResponseViewModel<bool>.Failure(
                   false,
                   $"An error occurred while adding the recipe: ",
                   ErrorCodeEnum.ServerError
               );

          
        }
    }
    }



