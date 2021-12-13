using System.Collections.Generic;

namespace TagsCloudVisualization.Default
{
    public class WordSelector : IWordSelector
    {
        private HashSet<string> stopWords;

        public WordSelector()
        {
            stopWords = new HashSet<string>()
            {
                "the", "be", "to", "of", "and", "a", "an", "in",
                "that", "for", "on", "as", "at", "by", "or", "and", "is", "are"
            };
        }

        public WordSelector(params string[] stopWords)
        {
            this.stopWords = new HashSet<string>(stopWords);
        }
        
        public IEnumerable<string> GetWords(string text)
        {
            var currentStart = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' || text[i] == '_' || char.IsDigit(text[i]) || char.IsLetter(text[i])) continue;
                if (currentStart != i)
                {
                    var word = text.Substring(currentStart, i - currentStart).ToLower();
                    if (!stopWords.Contains(word))
                        yield return word;
                } 
                currentStart = i + 1;
            }
            if (currentStart != text.Length)
            {
                var word = text.Substring(currentStart).ToLower();
                if (!stopWords.Contains(word))
                    yield return word;
            }
        }
    }
}