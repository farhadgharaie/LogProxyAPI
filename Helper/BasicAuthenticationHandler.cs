using LogProxyAPI.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LogProxyAPI.Helper
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private ILoginService _loginService;
        public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ILoginService loginService
        )
           : base(options, logger, encoder, clock)
        {
            _loginService = loginService;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                
                if (!_loginService.verify(username,password))
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Credential")); ;
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,username)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }
    }
}
