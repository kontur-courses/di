using System.Collections.Generic;

namespace TagCloud
{
    public class FrequencyTextAnalyzer : ITextAnalyzer
    {
        private readonly IEnumerable<Tag> words;
        
        public FrequencyTextAnalyzer(IEnumerable<Tag> words)
        {
            this.words = words;
        }
        
        public IEnumerable<TagInfo> GetWordsInfo()
        {
            var frequencyDictionary = new Dictionary<Tag, int>();
            
            foreach (var word in words)
            {
                if (frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] += 1;
                else
                    frequencyDictionary[word] = 1;
            }

            foreach (var word in frequencyDictionary.Keys)
                yield return new TagInfo(word, frequencyDictionary[word]);
        }
    }
}