using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    public delegate void ExceptionEventHandler(Exception ex);
    public static class ExceptionHandler
    {
        public static event ExceptionEventHandler ExceptionThrown;

        /*                                                    pure syntatic sugar                                       */
        internal static async void Notify(Exception ex) => await Task.Run(() => ExceptionThrown?.Invoke(ex ?? throw new ArgumentNullException("ex")));
    }
}
