using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public class User
    {
        public string Guid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string VerificationKey { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
