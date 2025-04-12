using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
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

namespace Application.CQRS.Recipes.Queries
{
    public record GetRecipesByNameOrTagOrCategoryQuery(GetRecipesByNameOrTagOrCategoryParamsDTO GetRecipesByNameOrTagOrCategoryParamsDTO) : IRequest<ResponseViewModel<IEnumerable<GetRecipesByNameOrTagOrCategoryDTO>>>;
    public class GetRecipesByNameOrTagOrCategoryQueryHandler : IRequestHandler<GetRecipesByNameOrTagOrCategoryQuery, ResponseViewModel<IEnumerable<GetRecipesByNameOrTagOrCategoryDTO>>>
    {
        private readonly IGeneralRepository<Recipe> _generalRepo;

        public GetRecipesByNameOrTagOrCategoryQueryHandler(IGeneralRepository<Recipe> generalRepo) 
        { 
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<IEnumerable<GetRecipesByNameOrTagOrCategoryDTO>>> Handle(GetRecipesByNameOrTagOrCategoryQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Recipe> recipes = _generalRepo.GetAll();
            GetRecipesByNameOrTagOrCategoryParamsDTO _params = request.GetRecipesByNameOrTagOrCategoryParamsDTO;
            if (_params.Name is not null)
            {
                recipes = recipes.Where(r => r.Name.Contains(_params.Name));
            }
            if (_params.Tag is not null)
            {
                recipes = recipes.Where(r => r.Tag == _params.Tag);
            }
            if (_params.Category is not null)
            {
                recipes = recipes.Where(r => r.Category == _params.Category);
            }
            var mappedRecipes = await recipes.Project<GetRecipesByNameOrTagOrCategoryDTO>().ToListAsync();
            if (mappedRecipes is null)
            {

                
                return ResponseViewModel<IEnumerable<GetRecipesByNameOrTagOrCategoryDTO>>.Failure(
                null,
               "Recipes not found.",
               ErrorCodeEnum.NotFound);

            }
            return ResponseViewModel<IEnumerable<GetRecipesByNameOrTagOrCategoryDTO>>.Success(mappedRecipes, "Recipes retrieved successfully.");
        }
    }
}
