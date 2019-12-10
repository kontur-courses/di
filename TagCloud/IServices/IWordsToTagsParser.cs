using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud
{
    public interface IWordsToTagsParser
    {
        List<Tag> GetTagsRectangles(Dictionary<string, int> words, ImageSettings imageSettings);
    }
}