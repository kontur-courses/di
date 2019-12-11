namespace TagCloud
{
    public static class StringExtension
    {
        public static string MakeLettersLowerCase(this string word)
        {
            return string.IsNullOrEmpty(word) ? string.Empty : word.ToLower();
        }

        public static string MakeLettersUpperCase(this string word)
        {
            return string.IsNullOrEmpty(word) ? string.Empty : word.ToUpper();
        }
    }
}