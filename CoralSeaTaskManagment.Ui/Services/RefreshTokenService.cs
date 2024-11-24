using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;

namespace CoralSeaTaskManagment.Services
{
    public class RefreshTokenService
    {
        //private readonly ProtectedLocalStorage protectedLocalStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string key="refresh_token";

        public RefreshTokenService(/*ProtectedLocalStorage protectedLocalStorage,*/ IHttpContextAccessor httpContext)
        {
            //this.protectedLocalStorage = protectedLocalStorage;
            this._httpContextAccessor = httpContext;
        }
        public async Task Set (string value)
        {
            await Task.Run(() =>
            {
                _httpContextAccessor.HttpContext.Session.SetString(key, value);
            });
            //await protectedLocalStorage.SetAsync(key, value);
        }
        public async Task<string> Get()
        {
            var result = _httpContextAccessor.HttpContext.Session.GetString  (key);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return string.Empty;
        }
        public async Task Remove( )
        {
            _httpContextAccessor.HttpContext.Session.Remove(key );
        }
    }
}
