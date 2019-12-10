using System.Linq;

namespace TagCloud
{
    public static class StringExtension
    {
        public static string MakeLettersLowerCase(this string word)
        {
            return string.IsNullOrEmpty(word) ? string.Empty : word.ToString().ToLower();
        }
    }
}