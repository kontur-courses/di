using System.Collections.Generic;
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

        public (bool isValid, string value) Filter(string stemmedString)
        {
            var standardForm = Analyzer.GetStandardForm(stemmedString);
            var partOfSpeech = Analyzer.DefinePartOfSpeech(stemmedString);
            var isValid = standardForm != string.Empty && WhiteList.Contains(partOfSpeech);
            return (isValid, standardForm);
        }

        public IEnumerable<string> GetFilteredValues(string textToFilter)
        {
            foreach (var stemmedString in Analyzer.AnalyzeText(textToFilter))
            {
                var (isValid, value) = Filter(stemmedString);
                if (isValid)
                    yield return value;
            }
        }
    }
}
