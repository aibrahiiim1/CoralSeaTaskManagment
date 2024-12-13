﻿using CoralSeaTaskManagment.Security;
using CoralSeaTaskManagment.Services;
using CoralSeaTaskManagment.Ui.Helper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class Dashboard : Controller
    {
        private readonly JWTAuthenticationStateProvider authStateProvider;
        private readonly AccessTokenServies accessTokenService;

        public Dashboard(JWTAuthenticationStateProvider AuthStateProvider,
           AccessTokenServies AccessTokenService)
        {
            authStateProvider = AuthStateProvider;
            accessTokenService = AccessTokenService;
        }
        public async Task< IActionResult> Index()
        {
          await  InitialValues();
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
