using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.ConsoleApp
{
    internal static class ExceptionHandler
    {
        public static void HandleExceptionsFrom(Action action, Action<Exception> handler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                handler(ex);
            }
        }
    }
}