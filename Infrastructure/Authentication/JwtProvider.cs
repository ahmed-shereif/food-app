using Domain.Contracts;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly jwtOptions _Options;

        public JwtProvider(IOptions<jwtOptions> options)
        {
            _Options = options.Value
                ;
        }

        public string GenerateToken(User user)
        {
            var _SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Options.SecretKey)), SecurityAlgorithms.HmacSha256);
            var _Claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var  token = new JwtSecurityToken(
                issuer: _Options.Issuer,
                audience: _Options.Audience,

                claims: _Claims,

                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: _SigningCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
