using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }
}