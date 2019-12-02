using System.Collections.Generic;

namespace TagsCloudVisualization.texts
{
    public class RepeatingWordsFilter : ITextFilter
    {
        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var uniqueWords = new List<string>();
            foreach (var word in words)
            {
                if(!uniqueWords.Contains(word))   
                    uniqueWords.Add(word);
            }

            return uniqueWords;
        }
    }
}