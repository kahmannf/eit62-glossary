using GlossaryDefinition;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    public class FileHandler<T> : IEnumerable<T> where T: IGuid, new()
    {
        private readonly Dictionary<string, T> store = new Dictionary<string, T>();

        private readonly object threadLock = new object();

        private readonly string directory;

        public int CachedItemCount => this.store.Count;

        public FileHandler(string directory)
        {
            this.directory = directory;

            DirectoryInfo dir = new DirectoryInfo(directory);

            if(!dir.Exists)
            {
                dir.Create();
            }
            else
            {
                FileInfo[] files = dir.GetFiles("*.json");

                foreach (FileInfo file in files)
                {
                    using (StreamReader reader = new StreamReader(new FileStream(file.FullName, FileMode.Open)))
                    {
                        string json = reader.ReadToEnd();

                        T item = JsonConvert.DeserializeObject<T>(json);

                        store.Add(item.Guid, item);
                    }
                }
            }
        }

        private Task InternalOverwrite(T item)
        {
            return Task.Run(() =>
            {
                lock (threadLock)
                {
                    store[item.Guid] = item;
                    DeleteFile(item.Guid);
                    CreateFile(item);
                }
            });
        }

        private Task InternalCreate(T item)
        {
            return Task.Run(() =>
            {
                lock (threadLock)
                {
                    store.Add(item.Guid, item);
                    CreateFile(item);
                }
            });
        }

        private Task InternalDelete(string guid)
        {
            return Task.Run(() =>
            {
                lock (threadLock)
                {
                    store.Remove(guid);
                    DeleteFile(guid);
                }
            });
        }

        private void CreateFile(T item)
        {
            string filename = Path.Combine(directory, item.Guid + ".json");

            using (StreamWriter writer = new StreamWriter(new FileStream(filename, FileMode.CreateNew)))
            {
                string json = JsonConvert.SerializeObject(item, Formatting.Indented);

                writer.Write(json);
            }
        }

        private void DeleteFile(string guid)
        {
            File.Delete(Path.Combine(directory, guid + ".json"));
        }

        public async Task<bool> Save(T item)
        {
            try
            {
                if (store.ContainsKey(item.Guid))
                {
                    await InternalOverwrite(item);
                }
                else
                {
                    await InternalCreate(item);
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.Notify(ex);
                return false;
            }
            return true;
        }

        public Task<bool> Delete(string guid)
        {
            return Task.Run(() =>
            {
                try
                {
                    InternalDelete(guid);
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Notify(ex);
                    return false;
                }

            });
        }

        public IEnumerator<T> GetEnumerator()
        {
            return store.Values.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return store.Values.ToArray().GetEnumerator();
        }

        public T GetByGuid(string guid)
        {
            try
            {
                return store[guid];
            }
            catch
            {
                return default(T);
            }
        }

        public int Count => store.Count;
    }
}
