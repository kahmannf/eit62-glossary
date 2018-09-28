using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace GlossaryBackend.JWT
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class JwtAuthenticatedAttribute : Attribute, IAuthenticationFilter
    {
        readonly string[] requiredScopes;
        
        public JwtAuthenticatedAttribute(string[] requiredScopes = null)
        {
            if(requiredScopes == null)
            {
                requiredScopes = new string[0];
            }

            this.requiredScopes = requiredScopes;
        }

        public string[] RequiredScopes
        {
            get { return requiredScopes; }
        }

        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeader = context.Request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrWhiteSpace(authHeader.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Access-token", context.Request);
                return Task.FromResult(0);
            }
            else
            {
                var principal = JwtManager.AuthenticateToken(authHeader.Parameter);

                if (principal == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid token", context.Request);
                }
                else
                {  
                    context.Principal = principal;   
                }
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            string parameter = "scopes=\"" + string.Join(",", requiredScopes) + "\"";

            context.ChallengeWith("Bearer", parameter);
            return Task.FromResult(0);
        }
    }
}