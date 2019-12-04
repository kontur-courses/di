using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Algorithm.WeightSetting
{
    public class FrequencyWeightSetter : IWordWeightSetter
    {
        public IEnumerable<Word> SetWordsWeights(IEnumerable<Word> words)
        {
            var wordsList = words.ToList();
            foreach (var word in wordsList)
            {
                var frequency = wordsList.Count(w => w.Value == word.Value);
                word.Weight = frequency;
                yield return word;
            }
        }
    }
}