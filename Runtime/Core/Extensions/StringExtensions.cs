using System;
using System.Text;
using System.Text.RegularExpressions;

namespace XIV.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for common string transformations.
    /// </summary>
    public static class StringExtensions
    {
        static readonly Regex PascalCaseRegex = new Regex(
            @"(?<=[a-z0-9])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])",
            RegexOptions.Compiled);

        static readonly Regex WordBoundaryRegex = new Regex(
            @"[\s_\-]+",
            RegexOptions.Compiled);

        public static string XIVSpace(this string value)
        {
            return value + " ";
        }

        public static string XIVPadCenter(this string str, int width)
        {
            if (string.IsNullOrEmpty(str)) str = "";
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(width);
        }

        public static string XIVTruncateWithDots(this string str, int maxLength)
        {
            return XIVEllipsify(str, maxLength);
        }

        /// <summary>
        /// Truncates the string to the specified maximum length and appends
        /// an ellipsis when the string exceeds that length.
        /// </summary>
        /// <param name="value">The string to ellipsify.</param>
        /// <param name="maxLength">The maximum resulting length, including the ellipsis.</param>
        /// <param name="ellipsis">The string to append when truncating.</param>
        public static string XIVEllipsify(this string value, int maxLength, string ellipsis = "...")
        {
            if (value == null)
                return null;

            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength));

            if (string.IsNullOrEmpty(ellipsis))
                throw new ArgumentException(
                    "Ellipsis cannot be null or empty.",
                    nameof(ellipsis));

            if (value.Length <= maxLength)
                return value;

            if (ellipsis.Length >= maxLength)
                return ellipsis.Substring(0, maxLength);

            return value.Substring(0, maxLength - ellipsis.Length) + ellipsis;
        }

        /// <summary>
        /// Splits PascalCase or camelCase words using the specified separator.
        /// </summary>
        /// <example>
        /// "helloWorld".SplitCamelCase(" ")
        /// => "hello World"
        /// </example>
        /// <example>
        /// "XMLHttpRequest".SplitCamelCase(" ")
        /// => "XML Http Request"
        /// </example>
        public static string XIVSplitCamelCase(this string value, string separator = " ")
        {
            return XIVSplitCase(value, separator);
        }

        /// <summary>
        /// Splits PascalCase words using the specified separator.
        /// </summary>
        /// <example>
        /// "HelloWorld".SplitPascalCase(" ")
        /// => "Hello World"
        /// </example>
        public static string XIVSplitPascalCase(this string value, string separator = " ")
        {
            return XIVSplitCase(value, separator);
        }

        /// <summary>
        /// Splits camelCase or PascalCase words using the specified separator.
        /// </summary>
        public static string XIVSplitCase(this string value, string separator = " ")
        {
            if (value == null)
                return null;

            if (string.IsNullOrEmpty(value))
                return value;

            separator ??= string.Empty;

            return PascalCaseRegex.Replace(value, separator);
        }

        /// <summary>
        /// Converts a string to camelCase.
        /// </summary>
        /// <example>
        /// "hello world".ToCamelCase() => "helloWorld"
        /// </example>
        /// <example>
        /// "HelloWorld".ToCamelCase() => "helloWorld"
        /// </example>
        public static string XIVToCamelCase(this string value)
        {
            var words = GetWords(value);

            if (words.Length == 0)
                return string.Empty;

            var builder = new StringBuilder(words[0].ToLowerInvariant());

            for (var i = 1; i < words.Length; i++)
            {
                builder.Append(Capitalize(words[i]));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a string to PascalCase.
        /// </summary>
        /// <example>
        /// "hello world".ToPascalCase() => "HelloWorld"
        /// </example>
        public static string XIVToPascalCase(this string value)
        {
            var words = GetWords(value);

            if (words.Length == 0)
                return string.Empty;

            var builder = new StringBuilder();

            foreach (var word in words)
                builder.Append(Capitalize(word));

            return builder.ToString();
        }

        /// <summary>
        /// Converts a string to snake_case.
        /// </summary>
        /// <example>
        /// "HelloWorld".ToSnakeCase() => "hello_world"
        /// </example>
        public static string XIVToSnakeCase(this string value)
        {
            return XIVSplitCase(value, "_").ToLowerInvariant();
        }

        /// <summary>
        /// Converts a string to kebab-case.
        /// </summary>
        /// <example>
        /// "HelloWorld".ToKebabCase() => "hello-world"
        /// </example>
        public static string XIVToKebabCase(this string value)
        {
            return XIVSplitCase(value, "-").ToLowerInvariant();
        }

        /// <summary>
        /// Converts a string to a space-separated title-like representation.
        /// </summary>
        /// <example>
        /// "helloWorld".ToWords() => "hello World"
        /// </example>
        public static string XIVToWords(this string value)
        {
            return XIVSplitCase(value, " ");
        }

        /// <summary>
        /// Merges words into camelCase.
        /// </summary>
        public static string XIVMergeCamelCase(this string value)
        {
            return value.XIVToCamelCase();
        }

        /// <summary>
        /// Merges words into PascalCase.
        /// </summary>
        public static string XIVMergePascalCase(this string value)
        {
            return value.XIVToPascalCase();
        }

        static string[] GetWords(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Array.Empty<string>();

            // First separate existing Pascal/camel case boundaries.
            value = XIVSplitCase(value, " ");

            // Then handle spaces, underscores and hyphens.
            return WordBoundaryRegex
                .Replace(value, " ")
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length == 1) return value.ToUpperInvariant();
            return char.ToUpperInvariant(value[0]) + value.Substring(1).ToLowerInvariant();
        }
    }
}