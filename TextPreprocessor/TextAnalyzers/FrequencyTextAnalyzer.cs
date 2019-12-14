using System.Collections.Generic;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextAnalyzers
{
    public class FrequencyTextAnalyzer : ITextAnalyzer
    {
        public IEnumerable<TagInfo> GetTagInfo(IEnumerable<Tag> tags)
        {
            var frequencyDictionary = new Dictionary<Tag, int>();
            
            foreach (var tag in tags)
            {
                if (frequencyDictionary.ContainsKey(tag))
                    frequencyDictionary[tag] += 1;
                else
                    frequencyDictionary[tag] = 1;
            }

            foreach (var word in frequencyDictionary.Keys)
                yield return new TagInfo(word, frequencyDictionary[word]);
        }
    }
}