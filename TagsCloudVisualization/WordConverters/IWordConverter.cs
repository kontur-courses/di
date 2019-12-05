using System.Collections.Generic;

namespace TagsCloudVisualization.WordConverters
{
    public interface IWordConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }
}