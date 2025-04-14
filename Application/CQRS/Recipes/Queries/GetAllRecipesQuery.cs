using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
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
    public record GetAllRecipesQuery(): IRequest<ResponseViewModel<IEnumerable<GetAllRecipesDto>>>;
    public class GetAllRecipesQueryHandler : IRequestHandler<GetAllRecipesQuery, ResponseViewModel<IEnumerable<GetAllRecipesDto>>>
    {
        private readonly IGeneralRepository<Recipe> _generalRepo;

        public GetAllRecipesQueryHandler(IGeneralRepository<Recipe> generalRepo)
        {
            _generalRepo = generalRepo;
        }


        public async Task<ResponseViewModel<IEnumerable<GetAllRecipesDto>>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes =  _generalRepo.GetAll();
            if (recipes is null)
            {

                return ResponseViewModel<IEnumerable<GetAllRecipesDto>>.Failure(
               null,
               "Recipes not found.",
               ErrorCodeEnum.NotFound);
            }

            
            var mappedRecipes = recipes.Map<IEnumerable<GetAllRecipesDto>>().ToList();
            return ResponseViewModel<IEnumerable<GetAllRecipesDto>>.Success(
                mappedRecipes,
                "Recipes retrieved successfully."
            );
        }
    }

   

}
