using System;
using System.Collections.Generic;
using TagsCloudVisualization.Structures;
using TagsCloudVisualization.WordAnalyzers;


namespace TagsCloudVisualization.Filters
{
    public class PartOfSpeechFilter: IFilter
    {
        public IMorphAnalyzer Analyzer { get; private set; }

        public PartOfSpeechFilter(IMorphAnalyzer analyzer)
        {
            Analyzer = analyzer;
        }

        public static List<string> WhiteList { get; private set; } = new List<string>
        {
            "A",
            "ADV",
            "ANUM",
            "APRO",
            "COM",
            "INTJ",
            "NUM",
            "S",
            "SPRO",
            "V"
        };

        public bool Filter(WordInfo wordInfo) 
            => wordInfo.StandartForm != string.Empty && WhiteList.Contains(wordInfo.PartOfSpeech);


        public IEnumerable<string> GetFilteredValues(string textToFilter)
        {
            foreach (var word in Analyzer.AnalyzeText(textToFilter))
            {
                var isValid = Filter(word);
                if (isValid)
                    yield return word.StandartForm;
            }
        }
    }
}
