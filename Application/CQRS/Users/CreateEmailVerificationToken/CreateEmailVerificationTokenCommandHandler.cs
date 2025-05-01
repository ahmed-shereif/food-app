using Application.DTOS.UserDtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Application.ViewModels.UserViewModels;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Users.CreateEmailVerificationToken
{
    public class CreateEmailVerificationTokenCommandHandler : IRequestHandler<CreateEmailVerificationTokenCommand, ResponseViewModel<EmailVerificationTokenDto>>
    {
        public readonly IGeneralRepository<EmailVerificationToken> _EmailVerificationgeneralRepository;

        public CreateEmailVerificationTokenCommandHandler(IGeneralRepository<EmailVerificationToken> emailVerificationgeneralRepository)
        {
            _EmailVerificationgeneralRepository = emailVerificationgeneralRepository;
        }

        public async Task<ResponseViewModel<EmailVerificationTokenDto>> Handle(CreateEmailVerificationTokenCommand request, CancellationToken cancellationToken)
        {
            var mappingToEmailVerificationToken = request.EmailVerificationTokenViewModel.Map<EmailVerificationToken>();

            mappingToEmailVerificationToken.Token =  Guid.NewGuid();
         
            await  _EmailVerificationgeneralRepository.AddAsync(mappingToEmailVerificationToken);
            var result = await _EmailVerificationgeneralRepository.SaveChangesAsync();

            var mappingToEmailVerificationTokenDto = mappingToEmailVerificationToken.Map<EmailVerificationTokenDto>();

            if (result <= 0)
            {
                return ResponseViewModel<EmailVerificationTokenDto>.Failure(mappingToEmailVerificationTokenDto, "EmailVerificationToken not created", ErrorCodeEnum.ServerError);
            }

            return ResponseViewModel<EmailVerificationTokenDto>.Success(mappingToEmailVerificationTokenDto, "EmailVerificationToken successfully");
        }
    }
}
