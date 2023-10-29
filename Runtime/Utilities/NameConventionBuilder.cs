using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lunha.DevKit.Utilities
{
    public static class NameConventionBuilder
    {
        public static string FormatTextByConvention(string text, NameConventionType convention)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            text = text.Replace('-', ' ');
            text = text.Replace('_', ' ');

            RemoveChar(ref text, '\'');
            RemoveChar(ref text, '\"');

            RemoveDoubleSpaces(ref text);

            text = text.Trim();
            text = text.Trim('.');

            switch (convention)
            {
                case NameConventionType.CamelCase:
                {
                    var output = Regex
                        .Replace(text, @"\b(\w)", match => match.Value.ToUpper())
                        .Replace(" ", "");

                    return char.ToLower(output[0]) + output.Substring(1);
                }
                case NameConventionType.SnakeCase:
                {
                    var output = Regex.Replace(text, @"(\p{Ll})(\p{Lu})|(\W)+", "$1_$2");
                    return output.ToLower();
                }
                case NameConventionType.KebabkCase:
                {
                    return Regex.Replace(text, @"(\p{Ll})(\p{Lu})|(\W)+", "$1-$2").ToLower();
                }
                case NameConventionType.PascalCase:
                {
                    var output = Regex.Replace(text, @"(?:^|\s)(\w)", match => match.Value.ToUpper());
                    return output.Replace(" ", "");
                }
                case NameConventionType.None:
                default:
                {
                    return text;
                }
            }
        }

        private static void RemoveChar(ref string text, char c)
        {
            for (var i = text.Count(e => e.Equals(c)); i > 0; i--)
            {
                text = text.Remove(text.IndexOf(c), 1);
            }
        }

        private static void RemoveDoubleSpaces(ref string text)
        {
            text = text.Trim();

            const string doubleSpace = "  ";
            while (text.Contains(doubleSpace))
            {
                var index = text.IndexOf(doubleSpace, StringComparison.Ordinal);
                text = text.Remove(index, 1);
            }
        }
    }

    public enum NameConventionType
    {
        /// <summary>
        /// No formatting would apply to provided user variant.
        /// </summary>
        None = 0,

        /// <summary>
        /// Example: firstName
        /// </summary>
        CamelCase = 1,

        /// <summary>
        /// Example: first_name
        /// </summary>
        SnakeCase = 2,

        /// <summary>
        /// Example: first-name 
        /// </summary>
        KebabkCase = 3,

        /// <summary>
        /// Example: FirstName
        /// </summary>
        PascalCase = 4,
    }
}