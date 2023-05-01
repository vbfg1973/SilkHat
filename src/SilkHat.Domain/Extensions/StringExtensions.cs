namespace SilkHat.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string NormaliseLineEndings(this string str)
        {
            return str.Replace("\\r", "");
        }
    }
}