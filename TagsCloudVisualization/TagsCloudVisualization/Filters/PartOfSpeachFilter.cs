using System.Collections.Generic;


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

        public bool IsValidValue(string value, string valueForFilter) 
            => value != string.Empty && partOfSpeechAdmissibility.ContainsKey(valueForFilter) && partOfSpeechAdmissibility[valueForFilter];
    }
}
