using System.Collections.Generic;

namespace WordCloudImageGenerator.Parsing.Extractors
{
    public interface IWordExtractor
    {
        IEnumerable<string> GetWords(string text);
    }
}