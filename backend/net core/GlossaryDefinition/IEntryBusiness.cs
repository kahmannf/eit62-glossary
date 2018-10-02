using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public interface IEntryBusiness
    {
        Task<Page<Entry>> GetEntries(int pageNumber, int pageSize, Func<Entry, bool>[] filters = null);
        Task<bool> SaveEntry(Entry entry);
        Task<Entry> GetEntry(string guid);
        Task<bool> DeleteEntry(string guid);
    }
}
