using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Security;
using CoralSeaTaskManagment.Services;
using CoralSeaTaskManagment.Ui.Helper;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    
    public class AuthController : Controller
    {        
       

        private readonly JWTAuthenticationStateProvider authStateProvider;
        private readonly AccessTokenServies accessTokenService;
        private readonly AuthService _authService;

        
        public AuthController(JWTAuthenticationStateProvider AuthStateProvider,
           AccessTokenServies AccessTokenService,
           AuthService AuthService)
        {
            authStateProvider = AuthStateProvider;
            accessTokenService = AccessTokenService;
            _authService = AuthService;
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {

            var status = await _authService.Login(authRequest.Username, authRequest.Password);
            if (status)
            {
                
                return RedirectToAction("Index", "Dashboard");

            }
            return View();
        }
        private async Task InitialValues()
        {
             await Task.Run(async () =>
            {
                var state = await authStateProvider.GetAuthenticationStateAsync();
                var user = state.User;
                if (user.Identity.IsAuthenticated)
                {

                    Identity.Token = await accessTokenService.GetToken();
                    Identity.Email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    Identity.Role = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                    Identity.FullName = user.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;
                    ViewBag.FullName = Identity.FullName;
                    Identity.DepId = Convert.ToInt32(user.Claims.FirstOrDefault(x => x.Type == "DepId")?.Value);
                    Identity.HotelId = Convert.ToInt32(user.Claims.FirstOrDefault(x => x.Type == "HotelId")?.Value);
                    var expires = user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
                    if (long.TryParse(expires, out var exp))
                    {
                        Identity.TokenExpired = DateTimeOffset.FromUnixTimeSeconds(exp).LocalDateTime;
                    }
                }
            });
           
        }

    }
}
