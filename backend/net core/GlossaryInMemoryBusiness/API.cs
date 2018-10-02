using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    public static class API
    {
        private static UserBusiness _userBusiness;

        public static IUserBusiness GetUserBusiness()
        {
            if(_userBusiness == null)
            {
                _userBusiness = new UserBusiness();
            }
            return _userBusiness;
        }

        private static EntryBusiness _entryBusiness;
        public static IEntryBusiness GetEntryBusiness()
        {
            if(_entryBusiness == null)
            {
                _entryBusiness = new EntryBusiness();
            }
            return _entryBusiness;
        }
    }
}
