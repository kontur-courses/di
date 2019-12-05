using System.Collections.Generic;

namespace TagsCloudGenerator.WordsHandler
{
    public interface IConverter
    {
        Dictionary<string, int> Convert(Dictionary<string, int> wordToCount);
    }
}