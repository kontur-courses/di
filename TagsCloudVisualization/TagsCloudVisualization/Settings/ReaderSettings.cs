using System.Collections.Generic;

namespace TagsCloudVisualization.Settings
{
    public class ReaderSettings
    {
        public ReaderSettings(string pathToText, int maxObjectsCount, string badWordsPath)
        {
            PathToText = pathToText;
            MaxObjectsCount = maxObjectsCount;
            BadWordsPath = badWordsPath;
        }

        public string BadWordsPath { get; }
        public string PathToText { get; }
        public int MaxObjectsCount { get; }
        public IEnumerable<string> BadWords { get; set; }
    }
}