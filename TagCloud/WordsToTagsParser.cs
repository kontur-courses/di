using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud
{
    public class WordsToTagsParser : IWordsToTagsParser
    {
        public List<Tag> GetTagsRectangles(Dictionary<string, int> words)
        {
            return new List<Tag>();
        }
    }
}