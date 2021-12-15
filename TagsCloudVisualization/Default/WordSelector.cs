using System.Collections.Generic;

namespace TagsCloudVisualization.Default
{
    public class WordSelector : IWordSelector
    {
        private static readonly HashSet<string> StopWords = new HashSet<string>()
        {
            "the", "be", "to", "of", "and", "a", "an", "in",
            "that", "for", "on", "as", "at", "by", "or", "and", "is", "are"
        };

        private static readonly HashSet<char> InsideWordSymbols = new HashSet<char>()
        {
            '.', '_', '-', '\''
        };

        public IEnumerable<string> GetWords(string text)
        {
            var currentStart = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (InsideWordSymbols.Contains(text[i]) || char.IsLetterOrDigit(text[i])) continue;
                if (currentStart != i)
                {
                    var word = TryGetWord(text, currentStart, i - 1);
                    if (word != null)
                        yield return word;
                } 
                currentStart = i + 1;
            }
            if (currentStart == text.Length) yield break;
            var lastWord = TryGetWord(text, currentStart, text.Length - 1);
            if (lastWord != null)
                yield return lastWord;
        }

        private static string TryGetWord(string text, int start, int end)
        {
            while (end > 0 && InsideWordSymbols.Contains(text[end]))
                end--;
            if (end - start + 1 <= 0) return null;
            var word = text.Substring(start, end - start + 1).ToLower();
            return !StopWords.Contains(word) ? word : null;
        }
    }
}