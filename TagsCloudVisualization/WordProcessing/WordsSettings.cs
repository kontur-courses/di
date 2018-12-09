using System;

namespace TagsCloudVisualization.WordProcessing
{
    public class WordsSettings
    {
        public string PathToFile { get; set; }
        public WordAnalyzer WordAnalyzer { get; set; }

        public WordsSettings()
        {
            PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/DefaultTags.txt";
            WordAnalyzer = new WordAnalyzer(this);
        }
    }
}