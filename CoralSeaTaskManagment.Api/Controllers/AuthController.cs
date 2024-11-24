using CoralSeaTaskManagment.Api.Infrastructure;

using CoralSeaTaskManagment.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CoralSeaTaskManagment.Api.Models.DTO;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenProvider tokenProvider;
        private readonly RefreshTokenHelper refreshTokenHelper;

        public AuthController(ApplicationDbContext  dbContext,
            TokenProvider tokenProvider,
            RefreshTokenHelper refreshTokenHelper)
        {
            this._dbContext = dbContext;
            this.tokenProvider = tokenProvider;
            this.refreshTokenHelper = refreshTokenHelper;
        }

        [HttpPost("Register")]
        public ActionResult Register(CreateUser User)
        {
            var PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.Password);
            var user = new Model.Models.Domain.User
            {
                Id=0,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Mobile = User.Mobile,
                Role = User.Role,
                PasswordHash = PasswordHash,
                Username= User.Username,
                DepartmentId= User.DepartmentId,
                HotelId= User.HotelId,
                Pic= User.Pic
            };

            // save user
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            //   _userService.Create(model);
            return Ok(new { message = "User created" });
        }
        [HttpPost("login")]
        public ActionResult<AuthResponse> Login( AuthRequest request)
        {
            var response=new AuthResponse();

            var username = request.Username;
            var password = request.Password;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _dbContext.Users 
                    .Where(p => p.Username == username || p.Email == username)
                    .FirstOrDefault();
                if (user == null)
                { //user not found
                    return Unauthorized("Username or password not correct");
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                { //password not correct
                    return Unauthorized("Username or password not correct");
                }

                //Generate Token
                var token=  tokenProvider.GenerateToken(user);
                response.Token = token.AccessToken;

                //Generate Refresh  Token
                response.RefreshToken = token.RefreshToken.Token;

                refreshTokenHelper.DisableUserTokenByEmail(user.Email);
                refreshTokenHelper.InsertRefreshToken(token.RefreshToken, user.Email);


                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost("refresh")]
        public ActionResult<AuthResponse> RefreshToken()
        {
            var response = new AuthResponse();
            var refershToken = Request.Cookies["refreshtoken"];

            if (string.IsNullOrEmpty(refershToken)) {  return BadRequest(); }  
            var isValid= refreshTokenHelper.IsRefreshTokenValid(refershToken);
            if (!isValid) { return BadRequest(); }

            var CurrentUser = refreshTokenHelper.FindUserByToken(refershToken);
            if (CurrentUser==null) { return BadRequest(); }
            //Generate Token
            var token = tokenProvider.GenerateToken(CurrentUser);
            response.Token = token.AccessToken;

            //Generate Refresh  Token
            response.RefreshToken = token.RefreshToken.Token;

            refreshTokenHelper.DisableUserToken(refershToken);
            refreshTokenHelper.InsertRefreshToken(token.RefreshToken, CurrentUser.Email);

            return Ok(response);
        }
        [HttpPost("logout")]
        public ActionResult Logout() {
            var refershToken = Request.Cookies["refreshtoken"];
            if (string.IsNullOrEmpty(refershToken)) { return BadRequest(); }
            refreshTokenHelper.DisableUserToken(refershToken);
            return Ok();
        }
    }
}
