using CoralSeaTaskManagment.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoralSeaTaskManagment.Security
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AccessTokenServies accessTokenServies;

        public JWTAuthenticationStateProvider(AccessTokenServies accessTokenServies)
        {
            this.accessTokenServies = accessTokenServies;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await accessTokenServies.GetToken();
                if (string.IsNullOrEmpty(token))
                    return await MarkAsUnauthorize();

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principal = new ClaimsPrincipal(identity);

                return await Task.FromResult(new AuthenticationState(principal));
            }
            catch (Exception ex)
            {
                return await MarkAsUnauthorize();

            }
        }

        private async Task<AuthenticationState> MarkAsUnauthorize()
        {
            try
            {
                var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }

}
