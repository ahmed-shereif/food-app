using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.UpdateUserEmailVerified
{
    public class UpdateEmailVerifiedCommandHandler : IRequestHandler<UpdateEmailVerifiedCommand,ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<User> _generalRepository;

        public UpdateEmailVerifiedCommandHandler(IGeneralRepository<User> generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<ResponseViewModel<bool>> Handle(UpdateEmailVerifiedCommand request, CancellationToken cancellationToken)
        {
            var existingUser= await _generalRepository.GetByIdWithTracking(request.UpdateEmailVerifiedStateViewModel.id);
            if (existingUser is null )
            {
                return ResponseViewModel<bool>.Failure(false, "user not found", ErrorCodeEnum.NotFound);
            }
            // Map Dto to recipe
            var updateUser = request.UpdateEmailVerifiedStateViewModel.Map<User>();


                _generalRepository.UpdateInclude(updateUser,nameof(User.EmailVerified));
              var reslt =await _generalRepository.SaveChangesAsync();
            // Check if the update was successful
            if(reslt > 0)
                return ResponseViewModel<bool>.Failure(false, "user not updated", ErrorCodeEnum.FailerUpdated);

            return ResponseViewModel<bool>.Success(true, "user updated successfully");
        }
    }
}
