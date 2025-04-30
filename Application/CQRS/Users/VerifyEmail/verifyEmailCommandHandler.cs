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

namespace Application.CQRS.Users.VerifyEmail
{
    internal class verifyEmailCommandHandler:IRequestHandler<verifyEmailCommand, ResponseViewModel<int>>
    {
        private readonly IGeneralRepository<EmailVerificationToken> _EmailVerificationgeneralRepository;

        public verifyEmailCommandHandler(IGeneralRepository<EmailVerificationToken> emailVerificationgeneralRepository)
        {
            _EmailVerificationgeneralRepository = emailVerificationgeneralRepository;
        }

        public async Task<ResponseViewModel<int>> Handle(verifyEmailCommand request, CancellationToken cancellationToken)
        {
            EmailVerificationToken?  token = await _EmailVerificationgeneralRepository
                                                    .Get(x => x.Token == request.TokenId)
                                                     .Select(x => new EmailVerificationToken
                                                     {   Id = x.Id,
                                                         ExpirationDate = x.ExpirationDate,
                                                         UserId = x.UserId 
                                                     }).FirstOrDefaultAsync();
            if (token is null || token.ExpirationDate < DateTime.UtcNow )
         //   if (token is null || token.ExpirationDate < DateTime.UtcNow || token.User.EmailVerified )
            {
                return ResponseViewModel<int>.Failure(token.UserId, "Invalid or expired token", ErrorCodeEnum.NotFound);
            } 
            
            var UserExist = await _EmailVerificationgeneralRepository
                                                    .Get(x => x.UserId == token.UserId)
                                                    .FirstOrDefaultAsync();
           // UserExist.User.EmailVerified = true;
           await _EmailVerificationgeneralRepository.Delete(token.Id);
           await _EmailVerificationgeneralRepository.SaveChangesAsync();
           return ResponseViewModel<int>.Success(token.UserId, "Email verified successfully");

        }
    }

}
