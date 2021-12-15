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

        public IEnumerable<string> GetWords(string text)
        {
            var currentStart = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' || text[i] == '_' || char.IsLetterOrDigit(text[i])) continue;
                if (currentStart != i)
                {
                    var word = text.Substring(currentStart, i - currentStart).ToLower();
                    if (!StopWords.Contains(word))
                        yield return word;
                } 
                currentStart = i + 1;
            }

            if (currentStart == text.Length) yield break;
            var lastWord = text.Substring(currentStart).ToLower();
            if (!StopWords.Contains(lastWord))
                yield return lastWord;
        }
    }
}