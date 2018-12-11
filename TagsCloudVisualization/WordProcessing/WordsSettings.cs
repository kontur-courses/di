using System;
using System.Collections.Generic;
using TagsCloudVisualization.WordProcessing.FileHandlers;

namespace TagsCloudVisualization.WordProcessing
{
    public class WordsSettings
    {
        public string PathToFile { get; set; }
        public WordAnalyzer WordAnalyzer { get; set; }
        public HashSet<string> BoringWords { get; set; }

        public WordsSettings()
        {
            var txtHandler =
                new TxtFileHandler($"{AppDomain.CurrentDomain.BaseDirectory}/ProjectFiles/BoringWords.txt");
            BoringWords = new HashSet<string>(txtHandler.ReadFile());
            PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/ProjectFiles/DefaultTags.txt";
            WordAnalyzer = new WordAnalyzer(this);
        }
    }
}