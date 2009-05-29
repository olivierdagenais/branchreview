using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Extension methods to augment implementations of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Concatenates a specified separator <see cref="String"/> between each element of a specified
        /// <see cref="IEnumerable{T}"/> of <see cref="Object"/>, yielding a single concatenated string. 
        /// </summary>
        /// 
        /// <param name="values">
        /// Zero or more <see cref="Object"/> instances, such as an array of <see cref="String"/>.
        /// </param>
        /// 
        /// <param name="separator">
        /// A <see cref="String"/> to insert in between all the <see cref="String"/> representations of the instances in
        /// <paramref name="values"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="String"/> consisting of the elements of <paramref name="values"/> interspersed with the
        /// <paramref name="separator"/> string.
        /// </returns>
        /// 
        /// <seealso cref="String.Join(String, String[])"/>
        public static string Join(this IEnumerable<object> values, string separator)
        {
            // TODO: consider providing an overload that accepts an IFormatProvider
            StringBuilder sb = new StringBuilder();
            var e = values.GetEnumerator();
            if (e.MoveNext())
            {
                sb.Append(e.Current);
                while (e.MoveNext())
                {
                    sb.Append(separator);
                    sb.Append(e.Current);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Prepares a single <see cref="String"/> representing all the <paramref name="values"/> quoted for use when
        /// invoking a sub-process.
        /// </summary>
        /// 
        /// <param name="values">
        /// Zero or more values to quote.
        /// </param>
        /// 
        /// <returns>
        /// All the <paramref name="values"/> converted to <see cref="String"/>, quoted and separated by spaces.
        /// </returns>
        public static string QuoteForShell(this IEnumerable<object> values)
        {
            // TODO: *nix shells might use single quotes?
            var joined = values.Join("\" \"");
            if (joined.Length > 0)
            {
                return "\"" + joined + "\"";
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
