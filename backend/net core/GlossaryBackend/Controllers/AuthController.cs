using GlossaryBackend.JWT;
using GlossaryBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace GlossaryBackend.Controllers
{
    public class AuthController : ApiController
    {
        [Route("api/auth/login")]
        [HttpGet]
        public TokenResponse Login()
        {
            if(Request == null 
                || Request.Headers == null 
                || Request.Headers.Authorization == null 
                || Request.Headers.Authorization.Scheme != "Basic"
                || string.IsNullOrWhiteSpace(Request.Headers.Authorization.Parameter))
            {
                throw HttpExceptionHelper.BadRequest;
            }

            byte[] data = Convert.FromBase64String(Request.Headers.Authorization.Parameter);
            string decodedString = Encoding.UTF8.GetString(data);

            int whitespace = decodedString.IndexOf(' ');

            if(whitespace < 1 )
            {
                throw HttpExceptionHelper.BadRequest;
            }

            string username = decodedString.Substring(0, whitespace);

            string password = decodedString.Substring(whitespace + 1);

            if(username?.ToLower() == "felix" && password == "test test")
            {
                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(JwtScopes.Scopes.Select(x => new Claim(ClaimTypes.Role, x))));

                TokenResponse response = new TokenResponse()
                {
                    refresh_token = JwtManager.GenerateToken(principal, new TimeSpan(30, 0, 0, 0), "top secret"),
                    access_token = JwtManager.GenerateToken(principal, new TimeSpan(0, 30, 0), "top secret")
                };

                return response;
            }
            else
            {
                throw HttpExceptionHelper.Unauthorized;
            }
            
        }
    }
}
