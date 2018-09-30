using System;

namespace CCA.Services.Goober.Library
{
    public static class StringExtensions
    {
        public static byte[] FromBase64Url(this string input)
        {
            if(string.IsNullOrEmpty(input)) return null;

            return Convert.FromBase64String(Pad(input.Replace('-', '+').Replace('_', '/')));
        }


        private static string Pad(string text)
        {
            var padding = 3 - ((text.Length + 3) % 4);
            if (padding == 0)
            {
                return text;
            }
            return text + new string('=', padding);
        }
    }
}
