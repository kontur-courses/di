using System.Collections.Generic;

namespace TagsCloudGenerator.WordsHandler.Converters
{
    public interface IConverter
    {
        Dictionary<string, int> Convert(Dictionary<string, int> wordToCount);
    }
}