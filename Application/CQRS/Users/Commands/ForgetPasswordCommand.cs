﻿using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Application.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(string Email) : IRequest<ResponseViewModel<string>>;

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResponseViewModel<string>>
    {
        private readonly IGeneralRepository<User> _userRepo;
        // private readonly IEmail _emailService; 


        public ForgotPasswordCommandHandler(IGeneralRepository<User> userRepo)
        {
            _userRepo = userRepo;

        }

       // Get Secret Key
        public string GetSecretKey(string Email)
        {
            return _userRepo.Get(x => x.Email == Email)
                 .Select(X => X.OTPSecretKey).
                 FirstOrDefault();


        }




        public async Task<ResponseViewModel<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.Get(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
                return ResponseViewModel<string>.Failure(null, "User not found", ErrorCodeEnum.NotFound);


            var Base32Key = GetSecretKey(request.Email);
            if (string.IsNullOrEmpty(Base32Key))
            {

                var otpSecretKey = KeyGeneration.GenerateRandomKey(20);
                Base32Key = Base32Encoding.ToString(otpSecretKey);
                user.OTPSecretKey = Base32Key;
                _userRepo.UpdateInclude(user, nameof(user.OTPSecretKey));
                await _userRepo.SaveChangesAsync();

            }

            var appName = "FoodAppSystem";
            var userName = "Rehab";
            // await _emailService.SendAsync(user.Email, "Reset Your Password", $"Your OTP is: {otp}");
            string otpUrl = $"otpauth://totp/{appName}:{userName}?secret={Base32Key}&issure={appName}";

            return ResponseViewModel<string>.Success(otpUrl, "OTP sent");
        }



    }

}
