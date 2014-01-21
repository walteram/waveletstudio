using System;

namespace DocumentationExtractor
{
    public static class Utils
    {
        public static string Append(this string text, string textToAppend)
        {
            return text + textToAppend;
        }

        public static string AppendLine(this string text, string textToAppend)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return textToAppend.TrimAll();
            }
            return text + Environment.NewLine + textToAppend.TrimAll();
        }

        public static string AppendLineSection(this string text, string textToAppend)
        {
            var value = textToAppend.Substring(textToAppend.IndexOf(':') + 1).TrimAll();
            if (string.IsNullOrWhiteSpace(text))
            {
                return value;
            }
            return text + Environment.NewLine + value;
        }

        public static string TrimAll(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            return text.Trim(' ', '\r', '\n', '\t');
        }
    }
}