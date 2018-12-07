using System.Collections.Generic;

namespace TagsCloudVisualization.Settings
{
    public class WordsExtractorSettings : IWordsExtractorSettings
    {
        public List<char> StopChars { get; set; }
        public List<string> StopWords { get; set; }
    }
}