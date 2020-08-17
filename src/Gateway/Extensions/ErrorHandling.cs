using System;

namespace Reviews.Extensions
{
    public static class ErrorHandling
    {
        public static bool True(Action action)
        {
            action();
            return true;
        }

        public static bool False(Action action)
        {
            action();
            return false;
        }
    }
}