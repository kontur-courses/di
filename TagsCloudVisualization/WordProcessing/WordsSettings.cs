using System;
using System.Collections.Generic;
using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.WordProcessing.FileHandlers;

namespace TagsCloudVisualization.WordProcessing
{
    public class WordsSettings : IWordsSettings
    {
        public string PathToFile { get; set; }
        public WordAnalyzer WordAnalyzer { get; set; }
        public HashSet<string> BoringWords { get; set; }

        public WordsSettings()
        {
            var txtHandler =
                new TxtFileHandler($"{AppDomain.CurrentDomain.BaseDirectory}/ProjectFiles/BoringWords.txt");
            BoringWords = new HashSet<string>(txtHandler.ReadFile());
            WordAnalyzer = new WordAnalyzer(this);
        }
    }
}