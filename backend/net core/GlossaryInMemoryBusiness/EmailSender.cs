using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    class EmailSender
    {
        public static async void SendVerificationEmail(User user)
        {
            await Task.FromResult(0);
        }

        public static async void SendPasswordResetEmail(User user)
        {
            await Task.FromResult(0);
        }
    }
}
