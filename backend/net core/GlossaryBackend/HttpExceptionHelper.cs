using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GlossaryBackend
{
    public static class HttpExceptionHelper
    {
        private static HttpResponseException GenerateException(HttpStatusCode code) => new HttpResponseException(new HttpResponseMessage(code) { Content = new StringContent(code.ToString()), ReasonPhrase = code.ToString() });

        public static HttpResponseException BadRequest => GenerateException(HttpStatusCode.BadRequest);
        public static HttpResponseException Unauthorized => GenerateException(HttpStatusCode.Unauthorized);


    }
}