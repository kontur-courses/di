using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Weighting
{
    public interface IWordsWeighter
    {
        Dictionary<string, Size> GetWordsSizes(List<string> words, Size minLetterSize);
    }
}