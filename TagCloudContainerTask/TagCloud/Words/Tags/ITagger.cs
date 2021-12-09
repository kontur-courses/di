using System.Collections.Generic;

namespace TagCloud.Words.Tags
{
    public interface ITagger
    {
        IEnumerable<Tag> CreateRawTags(Dictionary<string, double> wordsFrequencies, string fontName);
    }
}