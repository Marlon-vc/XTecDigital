using System;
using System.IO;
using System.Text.RegularExpressions;

namespace XTecDigital.Helpers
{
    public static class FileHandler
    {
        public static string StoragePath { get; set; }

        public static string GetGroupFolder(int numero, string curso, int anio, string periodo)
        {
            return Path.Combine(StoragePath, $"G{numero}-{curso}-{anio}-{periodo}");
        }

        public static byte[] FromBase64String(string input)
        {
            var index = input.IndexOf(",") + 1;
            if (index == -1)
                return Convert.FromBase64String(input);

            return Convert.FromBase64String(input[index..]);
        }
        
        /// <summary>
        /// Strip illegal chars and reserved words from a candidate filename (should not include the directory path)
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name
        /// </remarks>
        public static string CoerceValidFileName(this string filename)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidReStr = string.Format(@"[{0}]+", invalidChars);

            var reservedWords = new[]
            {
                "CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
                "COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
                "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
            };

            var sanitisedNamePart = Regex.Replace(filename, invalidReStr, "_");
            foreach (var reservedWord in reservedWords)
            {
                var reservedWordPattern = string.Format("^{0}\\.", reservedWord);
                sanitisedNamePart = Regex.Replace(sanitisedNamePart, reservedWordPattern, "_reservedWord_.", RegexOptions.IgnoreCase);
            }

            return sanitisedNamePart;
        }
    }
}