using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace CoralSeaTaskManagment.Api.Infrastructure
{
    public class RefreshTokenHelper
    {
        private readonly ApplicationDbContext dbContext;

        public RefreshTokenHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //InsertRefreshToken( RefreshToken,string Email)
        public bool InsertRefreshToken(RefreshAccessToken RefreshToken ,string Email )
        {
            var refreshToken = new RefreshToken
            {
                Token= RefreshToken.Token,
                CreateDate = RefreshToken.CreateDate,
                Enabled = RefreshToken.Enabled,
                Expires = RefreshToken.Expires,
                UserEmail= Email
            };
            dbContext.RefreshTokens.Add(refreshToken);
        var result=    dbContext.SaveChanges();
            return result>0;
        }
        //DisableUserTokenByEmail(string Email)
        public bool DisableUserTokenByEmail( string Email)
        {
            var token= dbContext.RefreshTokens.Where(p =>   p.UserEmail == Email)
                    .FirstOrDefault();
            if (token==null) return false;

            token.Enabled = false;
            dbContext.RefreshTokens.Update(token);
            var result = dbContext.SaveChanges();
            return result > 0;
        }

        //DisableUserToken(string token)
        public bool DisableUserToken(string token)
        {
            var Current = dbContext.RefreshTokens.Where(p => p.Token == token)
                    .FirstOrDefault();
            if (Current == null) return false;

            Current.Enabled = false;
            dbContext.RefreshTokens.Update(Current);
            var result = dbContext.SaveChanges();
            return result > 0;
        }
        //IsRefreshTokenValid(string token)
        public bool IsRefreshTokenValid(string token)
        {
            var Current = dbContext.RefreshTokens.Where(p => p.Token == token
            &&p.Enabled==true&&p.Expires>=DateTime.Now)
                    .FirstOrDefault();
            if (Current == null) return false;

           
            return true;
        }
        //FindUserByToken(string token)
        public User FindUserByToken(string token)
        {
            var user = (from u in dbContext.Users 
                        join t in dbContext.RefreshTokens on u.Email equals t.UserEmail
                        where t.Token == token
                        select u
                        ).FirstOrDefault();
            if(user == null) return null;
            return user;
        }
    }
}
