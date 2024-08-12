using System;

namespace GruInject.Core.Helpers
{
    public class DisposeChecker
    {
        public static bool IsDisposed(object obj)
        {
            try
            {
                obj.ToString();
                return false;
            }
            catch (ObjectDisposedException)
            {
                return true;
            }
        }
    }
}