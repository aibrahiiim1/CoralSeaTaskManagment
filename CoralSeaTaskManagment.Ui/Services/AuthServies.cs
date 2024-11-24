using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace CoralSeaTaskManagment.Services
{
    public class AuthService
    {
        private readonly AccessTokenServies accessToken;
        private readonly RefreshTokenService refreshTokenService;
        private readonly NavigationManager nav;
        private HttpClient client;

        public AuthService(AccessTokenServies accessToken, RefreshTokenService refreshTokenService,
            NavigationManager nav,
            IHttpClientFactory httpClientFactory)
        {
            this.accessToken = accessToken;
            this.refreshTokenService = refreshTokenService;
            this.nav = nav;
            client = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<bool> Login(string username, string password)
        {
            var status = await client.PostAsJsonAsync("auth/login", new { username, password });
            if (status.IsSuccessStatusCode)
            {
                var token = await status.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthResponse>(token);

                await accessToken.RemoveToken( );
                await accessToken.SetToken(result.Token);
                await refreshTokenService.Set(result.RefreshToken);
            //var x=    await refreshTokenService.Get( );
                return true;
            }
            else return false;
        }
        public async Task<bool> RefreshTokenAsync()
        {
            var refreshToken = await refreshTokenService.Get();
            client.DefaultRequestHeaders.Add("Cookie", $"refreshtoken={refreshToken}");
            var status = await client.PostAsync("auth/refresh", null);

            if (status.IsSuccessStatusCode)
            {
                var token = await status.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    var result = JsonConvert.DeserializeObject<AuthResponse>(token);
                    await accessToken.SetToken(result.Token);
                    await refreshTokenService.Set(result.RefreshToken);
                    return true;
                }

            }
            return false;
        }
        public async Task LogOut()
        {
            var refreshToken = await refreshTokenService.Get();
            client.DefaultRequestHeaders.Add("Cookie", $"refreshtoken={refreshToken}");
            var status = await client.PostAsync("auth/logout", null);
            if (status.IsSuccessStatusCode)
            {

                await accessToken.RemoveToken();
                await refreshTokenService.Remove();
                nav.NavigateTo("/auth/login", forceLoad: true);

            }
        }
    }
}
