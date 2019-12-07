using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TagsCloudContainer.WordProcessor;

namespace TagsCloudContainer.WordsToSizesConverter
{
    public interface IWordFrequenciesToSizesConverter
    {
        IEnumerable<Size> ConvertToSizes(IEnumerable<WordWithCount> wordsWithCount);
    }
}
