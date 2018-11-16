using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GlossaryBackend.JWT
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        readonly string[] scopes;

        // This is a positional argument
        public JwtAuthorizationAttribute(params string[] scopes)
        {
            if (scopes == null)
                scopes = new string[0];

            this.scopes = scopes;
        }

        public string[] Scopes
        {
            get { return scopes.ToArray(); }
        }

        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            ClaimsPrincipal principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if(principal == null)
            {
                actionContext.Response = GetUnauthorizedResponse(actionContext.Request);
                return Task.Run(() => actionContext.Response);
            }
            else
            {
                foreach(string scope in scopes)
                {
                    if (!principal.HasClaim(x => x.Type == "role" && x.Value == scope))
                    {
                        actionContext.Response = GetUnauthorizedResponse(actionContext.Request);
                        return Task.Run(() => actionContext.Response);
                    }
                }

                return continuation();
            }
        }

        private HttpResponseMessage GetUnauthorizedResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Content = new StringContent("<h1>Missing Scopes</h1>", Encoding.UTF8, "text/html");
            return response;
        }
    }
}