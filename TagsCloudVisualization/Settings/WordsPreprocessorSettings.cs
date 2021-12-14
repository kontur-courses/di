using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Settings
{
    public class WordsPreprocessorSettings
    {
        public string[] BoringWords { get; }

        public WordsPreprocessorSettings(IEnumerable<string> boringWords)
        {
            BoringWords = boringWords.ToArray();
        }
    }
}