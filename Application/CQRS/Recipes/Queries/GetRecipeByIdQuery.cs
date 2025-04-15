using Application.DTOS.RecipeDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;


namespace Application.CQRS.Recipes.Queries
{
   public  record GetRecipeByIdQuery(int id) :IRequest<ResponseViewModel<GetRecipeDto>>;

    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, ResponseViewModel<GetRecipeDto>>
    {
        private readonly IGeneralRepository<Recipe> _generalRepo;

        //constructor 
        public GetRecipeByIdQueryHandler(IGeneralRepository<Recipe> generalRepo) {
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
