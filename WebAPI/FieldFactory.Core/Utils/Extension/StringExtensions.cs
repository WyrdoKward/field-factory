using System;
using System.Linq;

namespace FieldFactory.Core.Utils.Extension
{
    public static class StringExtensions
    {
        /// <summary>
        /// Upper only the first character of input string
        /// </summary>
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
    }
}
