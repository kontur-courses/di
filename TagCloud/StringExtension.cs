using System.Linq;

namespace TagCloud
{
    public static class StringExtension
    {
        public static string MakeFirstLetterLowerCase(this string word)
        {
            if (string.IsNullOrEmpty(word)) return string.Empty;
            return word.First().ToString().ToLower() + word.Substring(1);
        }
    }
}