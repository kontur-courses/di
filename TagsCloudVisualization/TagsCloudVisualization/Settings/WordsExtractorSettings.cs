using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Settings
{
    public class WordsExtractorSettings : IWordsExtractorSettings
    {
        public List<char> StopChars { get; set; }
        public List<string> StopWords { get; set; }
    }
}