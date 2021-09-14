using System.Text;

namespace StackExchangeNetCoreTest.Extensions
{
	internal static class StringExtensions
	{
		/// <summary>
		/// Limits the string to be no longer than the specified max length.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="maxLength">Length of the max.</param>
		/// <param name="addContinuationInfo">Adds "...[# of chars removed = {1}]" to the end of string if it is trimmed.</param>
		/// <returns></returns>
		public static string Limit(this string value, int maxLength, bool addContinuationInfo = false)
		{
			return (string.IsNullOrEmpty(value) || value.Length <= maxLength)
				? value
				: (
					addContinuationInfo
					? string.Format(
						"{0}...[# of chars removed = {1}]",
						value.Substring(0, maxLength),
						value.Length - maxLength
					)
					: value.Substring(0, maxLength)
				);
		}

		public static string FormatX(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

        public static string FromUtf8Bytes(this byte[] bytes)
        {
            return bytes == null ? null
                : Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] ToUtf8Bytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

    }
}
