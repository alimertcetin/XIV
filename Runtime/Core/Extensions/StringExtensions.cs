namespace XIV.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Space(this string value)
        {
            return value + " ";
        }

        public static string PadCenter(this string str, int width)
        {
            if (string.IsNullOrEmpty(str)) str = "";
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(width);
        }

        public static string TruncateWithDots(this string str, int maxLength)
        {
            return TruncateWithChar(str, maxLength, '.');
        }

        public static string TruncateWithChar(this string str, int maxLength, char c)
        {
            if (string.IsNullOrEmpty(str) || maxLength < 4)
                return (str ?? "").PadRight(maxLength); // fallback

            return str.Length > maxLength
                ? str.Substring(0, maxLength - 3) + new string(c, 3)
                : str;
        }
    }
}