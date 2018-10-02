using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    class EntryBusiness : IEntryBusiness
    {
        public Task<bool> DeleteEntry(string guid)
        {
            throw new NotImplementedException();
        }

        public Task<Page<Entry>> GetEntries(int pageNumber, int pageSize, Func<Entry, bool>[] filters)
        {
            throw new NotImplementedException();
        }

        public Task<Entry> GetEntry(string guid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntry(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
