using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public class StopWordsFilter : IWordsFilter
    {
        private HashSet<string> stopWords = new HashSet<string> { "a", "the", "in", "of", "for", "and", "but" };

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower()).Where(word => !stopWords.Contains(word)).ToList();
        }

        public void AddStopWord(string stopWord)
        {
            stopWords.Add(stopWord);
        }

        public void RemoveStopWord(string stopWord)
        {
            stopWords.Remove(stopWord);
        }
    }
}
