using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Token
{
    public class TokenGenerationCommandHandler : IRequestHandler<TokenGenerationCommand, string>
    {
        private readonly IJwtProvider _jwtProvider;
        public TokenGenerationCommandHandler( IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Handle(TokenGenerationCommand request, CancellationToken cancellationToken)
        {
         string token = _jwtProvider.GenerateToken(request.user);
            return token;
        }
    
    }
}
