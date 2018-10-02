using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public class User : IGuid
    {
        public string Guid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string VerificationKey { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
