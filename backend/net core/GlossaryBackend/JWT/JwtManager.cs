﻿using System;
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
        //Generated by:
        //System.Security.Cryptography.TripleDESCryptoServiceProvider bla = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
        //string temp = Convert.ToBase64String(bla.Key);
        private const string SECRET = "TkluQ3hTak9OQ3BsWlRqTjFhamZ6ckRaWnU1RFFXcGlzZGZnc2RmZ3NkbGZnKCk2YsOfOTgvJkJGSkZKWlQ1dDZyNjdpYlImL09SVms3dGwoVC9ST0xQJSQ4bzZhc2Q1NnM0ZGdmMzQ1aGYtLiw8cGZnPHJldMO8cjBhbmVyZ2RmaGtsLsO2bmU0N3R2N3PDtmVyOHpzZ2huZmQuZ3NlNTh6dG5ic2Vyw7ZwdGw5djgzNHpucDM4NDZ6bmIgIHEyMzRww7bDtnR6OGVybmhzZ2R1ZnNnbGVyOHo1dHEzNMO2bHJlczhnaGrDtnNmOG9sdXp3ZcO2cjhvaWZn";

        public static string GenerateToken(ClaimsPrincipal principal, TimeSpan expiresIn)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] symmetricKey = Convert.FromBase64String(SECRET);

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.Add(expiresIn),
                Subject = new ClaimsIdentity(principal.Claims),
                SigningCredentials = credentials
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

            tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                RequireSignedTokens = true,
                

            },out SecurityToken scToken);

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