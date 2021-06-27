using System;

namespace Infrastructure.String
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static void ThrowIfNullOrEmpty(this string value, Exception ex)
        {
            if (value.IsNullOrEmpty()) throw ex;
        }
    }
}
