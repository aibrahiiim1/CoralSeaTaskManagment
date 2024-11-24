namespace CoralSeaTaskManagment.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Set(string key, string value, int? expireTime)
        {
            await Task.Run(() =>
            {
                CookieOptions option = new CookieOptions();
                if (expireTime.HasValue)
                {
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                }
                else
                {
                    option.Expires = DateTime.Now.AddMilliseconds(10);
                }
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
            }          
            );
        }
        public async Task<string> Get(string key)
        {
            return await Task.Run(()=> _httpContextAccessor.HttpContext.Request.Cookies[key]); 
        }
        public async Task Remove(string key)
        {
            await Task.Run(() => _httpContextAccessor.HttpContext.Response.Cookies.Delete(key)); 
        }
    }
    //public class CookieService
    //{
    //    private readonly IJSRuntime iJSRunTime;

    //    public CookieService(IJSRuntime iJSRunTime)
    //    {
    //        this.iJSRunTime = iJSRunTime;
    //    }
    //    public async Task<string> Get(string key)
    //    {
    //        return await iJSRunTime.InvokeAsync<string>("getCookie", key);
    //    }
    //    public async Task Remove(string key)
    //    {
    //        await iJSRunTime.InvokeVoidAsync("deleteCookie", key);
    //    }
    //    public async Task Set(string key, string value, int days)
    //    {
    //        await iJSRunTime.InvokeVoidAsync("setCookie", key, value, days);
    //    }
    //}
}
