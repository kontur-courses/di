using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing.Converting
{
    public interface IWordConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }
}