using System;
using System.Diagnostics.CodeAnalysis;

namespace Joker.Exceptions
{
    public static class Check
    {
        public static void NotNull(object value, [NotNull] string paramName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        
        public static void NotEmpty(Guid value, [NotNull] string paramName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(paramName);
            }
        }
        
        public static void NotNullOrEmpty(string value, [NotNull] string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}