using System.Collections.Generic;

namespace TagCloud.Creators
{
    public interface ITagCreator
    {
        Tag Create(string value, int frequency);
        IEnumerable<Tag> Create(Dictionary<string, int> wordsWithFrequency);
    }
}
