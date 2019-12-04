using System.Collections.Generic;

namespace TagsCloudContainer.Algorithm.WeightSetting
{
    public interface IWordWeightSetter
    {
        IEnumerable<Word> SetWordsWeights(IEnumerable<Word> words);
    }
}