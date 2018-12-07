using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Settings
{
    public class WordsExtractorSettingsProvider
    {
        public static WordsExtractorSettings GetDefaultSettings()
        {
            return new WordsExtractorSettings
            {
                StopChars = "?@,.;)(:0123456789".ToCharArray().ToList(),
                StopWords = new List<string>
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
                }
            };
        }
    }
}