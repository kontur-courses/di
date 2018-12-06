using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Settings
{
    public class WordsExtractorSettings : IWordsExtractorSettingsProvider
    {
        public List<char> StopChars { get; set; } = "?@,.;)(:0123456789".ToCharArray().ToList();

        public List<string> StopWords { get; set; } = new List<string>
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