using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordsExtractorSettings
    {
        public List<char> StopChars = "?@,.;)(:0123456789".ToCharArray().ToList();

        public List<string> StopWords = new List<string>
        {
            "the",
            "and",
            "to",
            "a",
            "of",
            "in",
            "on",
            "at",
            "that",
            "as",
            "but",
            "with",
            "out",
            "for",
            "up",
            "one",
            "from",
            "into"
        };
    }
}