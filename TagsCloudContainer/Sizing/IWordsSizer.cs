using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Sizing
{
    public interface IWordsSizer
    {
        Dictionary<string, Size> GetWordsSizes(List<string> words, Size minLetterSize);
    }
}