﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CoralSeaTaskManagment.Security
{
    public class JWTAuthenticationHandler : AuthenticationHandler<CustOption>
    {
        public JWTAuthenticationHandler(IOptionsMonitor<CustOption> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = Request.Cookies["access_token"];
                if (string.IsNullOrEmpty(token))
                    return AuthenticateResult.NoResult();

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principal = new ClaimsPrincipal(identity);

                var ticket=new AuthenticationTicket(principal,Scheme.Name);
                return AuthenticateResult.Success(ticket);
             }
            catch (Exception ex )
            {

                return AuthenticateResult.NoResult();
            }
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/auth/login");
            return Task.CompletedTask;
        }
        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/accessdenied");
            return Task.CompletedTask;
        }
    }

    public class CustOption:AuthenticationSchemeOptions
    {
    }
}