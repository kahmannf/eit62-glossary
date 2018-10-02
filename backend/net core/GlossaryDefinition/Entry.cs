using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public class Entry : IGuid
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public Entry[] Links { get; set; }
    }
}
