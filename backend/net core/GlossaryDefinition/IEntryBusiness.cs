using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    interface IEntryBusiness
    {
        Page<Entry> GetEntries(int pageNumber, int pageSize, Func<Entry, bool>[] filters);
        bool SaveEntry(Entry entry);
        Entry GetEntry(string guid);
        bool DeleteEntry(string guid);
    }
}
