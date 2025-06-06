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
    }
}