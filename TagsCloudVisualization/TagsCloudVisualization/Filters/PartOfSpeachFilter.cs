using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TagsCloudVisualization.Filters
{

    class PartOfSpeachFilter: IFilter
    {
        private static Dictionary<string, bool> partOfSpeechAdmissibility = new Dictionary<string, bool>()
        {
            {"A", true },
            {"ADV", true },
            {"ADVPRO", true },
            {"ANUM", true },
            {"APRO", true },
            {"COM", true },
            {"CONJ", false },
            {"INTJ", true },
            {"NUM", true },
            {"PART", false },
            {"PR", false },
            {"S", true },
            {"SPRO", true },
            {"V", true }
        };

        private readonly Regex regExForStandardWordForm = new Regex("\"lex\":\"(\\w+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex regExForPartOfSpeech = new Regex("\"gr\":\"(\\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public (bool isValid, string value) Filter(string stemmedString)
        {
            var standardForm = regExForStandardWordForm.Match(stemmedString).Groups[1].Value;
            var partOfSpeech = regExForPartOfSpeech.Match(stemmedString).Groups[1].Value;

            var isValid = standardForm != string.Empty && partOfSpeechAdmissibility.ContainsKey(partOfSpeech) &&
                partOfSpeechAdmissibility[partOfSpeech];
            return (isValid, standardForm);
        }
    }
}
