using System.Collections.Generic;

namespace TagsCloudContainer.Algorithm
{
    public interface IWordWeightSetter
    {
        IEnumerable<Word> SetWordsWeights(IEnumerable<Word> words);
    }
}