using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlossaryBackend.Models
{
    public class TokenResponse
    {
        public string refresh_token { get; set; }
        public string access_token { get; set; }
    }
}