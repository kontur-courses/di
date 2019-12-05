using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.WordsToSizesConverter
{
    public interface IWordFrequenciesToSizesConverter
    {
        IList<Size> ConvertToSizes(IDictionary<string, int> wordFrequencies);
    }
}
