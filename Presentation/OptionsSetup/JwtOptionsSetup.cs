using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<jwtOptions>
    {
        private readonly IConfiguration _Configuration;
        private readonly string SectionName = "Jwt";
        public JwtOptionsSetup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public void Configure(jwtOptions options)
        {
            _Configuration.GetSection(SectionName).Bind(options);
        }
    }
}
