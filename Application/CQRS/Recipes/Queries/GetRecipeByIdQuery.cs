using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Queries
{
   public  record GetRecipeByIdQuery(int id) :IRequest<ResponseViewModel<GetRecipeDto>>;

    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, ResponseViewModel<GetRecipeDto>>
    {
        private readonly GeneralRepository<Recipe> _generalRepo;

        //constructor 
        public GetRecipeByIdQueryHandler(GeneralRepository<Recipe> generalRepo) {
            _generalRepo = generalRepo;
        }

        public async Task<ResponseViewModel<GetRecipeDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _generalRepo.GetByIdAsync(request.id);
            if (recipe == null) {

                return ResponseViewModel<GetRecipeDto>.Failure(
               null,
               "Recipe not found.",
               ErrorCodeEnum.NotFound);
            }

            var mappedRecipe = recipe.Map<GetRecipeDto>();

            return ResponseViewModel<GetRecipeDto>.Success(
                mappedRecipe,
                "Recipe retrieved successfully."
            );


        }
    }
}
