using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{

    [Serializable]
    public class MissingConfigException : Exception
    {
        public string ConfigField { get; set; }
        public MissingConfigException(string configField) { ConfigField = configField; }
        public MissingConfigException(string configField, string message) : base(message) { ConfigField = configField; }
        protected MissingConfigException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
