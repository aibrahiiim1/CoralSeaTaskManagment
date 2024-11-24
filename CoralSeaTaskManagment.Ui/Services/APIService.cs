using CoralSeaTaskManagment.Ui.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text;

namespace CoralSeaTaskManagment.Services
{
    public class APIService
    {
        private readonly HttpClient client;
        private readonly AccessTokenServies tokenServies;
        private readonly AuthService authService;
        private readonly NavigationManager nav;

        public APIService(IHttpClientFactory httpClientFactory,
            AccessTokenServies tokenServies,
            AuthService authService,
            NavigationManager nav)
        {
            client = httpClientFactory.CreateClient("ApiClient");
            this.tokenServies = tokenServies;
            this.authService = authService;
            this.nav = nav;
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {



            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.GetAsync(endpoint);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.GetAsync(endpoint);
                return newResponse;
                
            }
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostDataAsync(string endpoint, object obj=null)
        {

            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endpoint),
            };
            if (obj != null)
            {
                httpRequestMessage.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj),
                Encoding.UTF8, "application/json");
            }
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.SendAsync(httpRequestMessage);
                return newResponse;
            }
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PutDataAsync(string endpoint, object obj=null)
        {

            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(endpoint),
                
            };
            if (obj!=null)
            {
                httpRequestMessage.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj),
                Encoding.UTF8, "application/json");
            }
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.SendAsync(httpRequestMessage);
                return newResponse;
            }
            return httpResponseMessage;
        }



        public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
        {

            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.DeleteAsync(endpoint);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.DeleteAsync(endpoint);
                return newResponse;
            }
            return responseMessage;
        }
    }
}
/*
         public async Task<HttpResponseMessage> PostDataAsync(string endpoint, object obj)
        {

            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.PostAsJsonAsync(endpoint, obj);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.PostAsJsonAsync(endpoint, obj);
                return newResponse;
            }
            return responseMessage;
        }

        public async Task<HttpResponseMessage> PutDataAsync(string endpoint, object obj)
        {

            var token = await tokenServies.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.PutAsJsonAsync(endpoint, obj);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //call refresh token
                var refreshTokenResult = await authService.RefreshTokenAsync();
                if (!refreshTokenResult)
                    await authService.LogOut();
                var newToken = await tokenServies.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await client.PutAsJsonAsync(endpoint, obj);
                return newResponse;
            }
            return responseMessage;
        }
 
 */