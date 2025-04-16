using Application.Helpers;
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

namespace Application.CQRS.Users.Commands
{
    
    public record ResetPasswordCommand(string Email, string OTP, string NewPassword) : IRequest<ResponseViewModel<bool>>;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<User> _userRepository;
        public ResetPasswordCommandHandler(IGeneralRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseViewModel<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => x.Email == request.Email ).FirstOrDefaultAsync();
            if (user == null || user.OTPSecretKey != request.OTP)
                return ResponseViewModel<bool>.Failure(false, "Invalid OTP or email", ErrorCodeEnum.NotFound);

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword); 
            user.OTPSecretKey = null;

            _userRepository.UpdateInclude(user, nameof(user.Password), nameof(user.OTPSecretKey));
            await _userRepository.SaveChangesAsync();

            return ResponseViewModel<bool>.Success(true, "Password reset successfully");
        }
    }

}
