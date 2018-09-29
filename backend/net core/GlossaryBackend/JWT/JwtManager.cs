using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GlossaryBackend.JWT
{
    public static class JwtManager
    {
        public static string GenerateToken(ClaimsPrincipal principal, TimeSpan expiresIn, string secret, string algorithm = SecurityAlgorithms.HmacSha256Signature)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity tokenIdentity = new ClaimsIdentity(principal.Claims);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.Add(expiresIn),
                Subject = tokenIdentity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), algorithm)
            };

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal AuthenticateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
                return null;

            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);

            if(jwt != null && jwt.Claims != null)
            {
                return new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims));
            }
            else
            {
                return null;
            }
        }
    }
}