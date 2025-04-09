using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Commands
{
    public record RemoveRecipeCommand(int id):IRequest<ResponseViewModel<bool>>;
    public class RemoveRecipeCommandHandler : IRequestHandler<RemoveRecipeCommand,ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<Recipe> _generalRepo;

        public RemoveRecipeCommandHandler(IGeneralRepository<Recipe> generalRepo) {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var recipe = await _generalRepo.GetByIdAsync(request.id);
                if (recipe == null)
                {
                    return ResponseViewModel<bool>.Failure(false, "Recipe not found", ErrorCodeEnum.NotFound);
                }

                await _generalRepo.Delete(request.id);
                await _generalRepo.SaveChangesAsync();


                return ResponseViewModel<bool>.Success(true, "Recipe deleted successfully.");
            }
            catch (Exception ex)
            {
                
                return ResponseViewModel<bool>.Failure(false, "An error occurred while deleting the recipe.", ErrorCodeEnum.ServerError);
            }




        }
    }
}
