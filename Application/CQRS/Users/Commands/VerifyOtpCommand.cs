using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.Commands
{
   public record VerifyOtpCommand(string otp,string email) : IRequest<ResponseViewModel<bool>>;
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<User> _userRepo;
        public VerifyOtpCommandHandler(IGeneralRepository<User>  userRepo) { 
            _userRepo = userRepo;

        }

        public string GetSecretKey(string Email)
        {
            return _userRepo.Get(x => x.Email == Email)
                 .Select(X => X.OTPSecretKey).
                 FirstOrDefault();


        }
        public async Task<ResponseViewModel<bool>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.Get(x => x.Email == request.email).FirstOrDefaultAsync();
            if (user == null)
                return ResponseViewModel<bool>.Failure(false, "User not found", ErrorCodeEnum.NotFound);
            var base32Key = GetSecretKey(request.email);
            if (string.IsNullOrEmpty(base32Key))
            {
                return ResponseViewModel<bool>.Failure(false, "Not Verified");
            }

            var key = Base32Encoding.ToBytes( base32Key);
            var totp = new Totp(key);
            var isValid = totp.VerifyTotp(request.otp, out _);
            return ResponseViewModel<bool>.Success(isValid ,"Verified otp");
        }
    }
}
