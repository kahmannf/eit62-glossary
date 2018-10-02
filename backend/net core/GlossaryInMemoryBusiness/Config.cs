using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    public class Config
    {
        private static Config _instance;
        public static Config GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Config();
            }

            return _instance;
        }

        private Config() { }

        public string DataDirectory { get; set; }
    }
}
