using Application.DTOS.FavoriteDtos;
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

namespace Application.CQRS.Favorites.Queries
{
    public record GetFavoriteRecipesByUserIdQuery(int UserId) : IRequest<ResponseViewModel<IEnumerable<GetRecipeUserFavoritesDTO>>>;
    public class GetFavoriteRecipesByUserIdQueryHandler : IRequestHandler<GetFavoriteRecipesByUserIdQuery, ResponseViewModel<IEnumerable<GetRecipeUserFavoritesDTO>>>
    {
        private readonly IGeneralRepository<RecipeUserFavorites> _generalRepo; 
        public GetFavoriteRecipesByUserIdQueryHandler(IGeneralRepository<RecipeUserFavorites> generalRepo)
        {
            _generalRepo = generalRepo;
        }

        public async Task<ResponseViewModel<IEnumerable<GetRecipeUserFavoritesDTO>>> Handle(GetFavoriteRecipesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _generalRepo.Get(x => x.UserId == request.UserId).Project<GetRecipeUserFavoritesDTO>().ToListAsync();
            if (result is null || result.Count() == 0)
            {
                return ResponseViewModel<IEnumerable<GetRecipeUserFavoritesDTO>>.Failure(
                    null,
                    $"No favorite recipes found for user with ID {request.UserId}.",
                    ErrorCodeEnum.NotFound
                );
            }
            return ResponseViewModel<IEnumerable<GetRecipeUserFavoritesDTO>>.Success(result, "Favorite recipes retrieved successfully.");

        }
    }
}
