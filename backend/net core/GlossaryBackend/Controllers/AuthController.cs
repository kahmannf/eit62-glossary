using GlossaryBackend.JWT;
using GlossaryBackend.Models;
using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GlossaryBackend.Controllers
{
    public class AuthController : ApiController
    {
        IUserBusiness userBusiness = WebApiApplication.GetUserBusiness();

        [Route("api/auth/login")]
        [HttpGet]
        public async Task<TokenResponse> Login()
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

            string email = decodedString.Substring(0, whitespace);

            string base64Password = decodedString.Substring(whitespace + 1);

            byte[] password = Convert.FromBase64String(base64Password);

            if (await userBusiness.CheckLoginData(email, password))
            {
                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(JwtScopes.Scopes.Select(x => new Claim(ClaimTypes.Role, x))));

                ClaimsPrincipal refreshPrincpal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("email", email) }));

                TokenResponse response = new TokenResponse()
                {
                    refresh_token = JwtManager.GenerateRefreshToken(refreshPrincpal, new TimeSpan(30, 0, 0, 0)),
                    access_token = JwtManager.GenerateAccessToken(principal, new TimeSpan(0, 30, 0))
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
