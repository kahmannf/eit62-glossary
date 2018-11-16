using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    class EntryBusiness : IEntryBusiness
    {
        FileHandler<Entry> _filehandler = new FileHandler<Entry>(Path.Combine(Config.GetInstance().DataDirectory ?? throw new MissingConfigException("DataDirectory"), "entries"));

        public Task<bool> DeleteEntry(string guid)
        {
            return _filehandler.Delete(guid);
        }

        public Task<Page<Entry>> GetEntries(int index, int size, Func<Entry, bool>[] filters)
        {

            Page<Entry> result = new Page<Entry>
            {
                Index = index,
                Size = size
            };

            try
            {
                Func<Entry, bool> filter = e => true;

                if (filters != null && filters.Length > 0)
                {
                    if (filters.Length == 1)
                    {
                        filter = filters[1];
                    }
                    else
                    {
                        filter = filters.Aggregate((x, y) => e => x(e) && y(e));
                    }
                }

                result.Items = _filehandler.Where(x => filter(x)).Skip(index * size).Take(size).ToArray();
                result.Total = _filehandler.Count;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Notify(ex);
            }
            return Task.FromResult(result);
        }

        public Task<Entry> GetEntry(string guid)
        {
            return Task.FromResult(_filehandler.GetByGuid(guid));
        }

        public Task<bool> SaveEntry(Entry entry)
        {
            return _filehandler.Save(entry);
        }
    }
}
