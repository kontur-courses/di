using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TagsCloudContainer.PreprocessingWords
{
    internal class MyStemOutput
    {
        [JsonProperty("text")] 
        public string Text { get; set; }

        [JsonProperty("analysis")] 
        public WordInfo[] Analysis { get; set; }


        // тут  все возможные за исключением существительного
        // "S",	существительное
        private readonly HashSet<string> overlookedPartsOfSpeech = new HashSet<string>
        {
            "A", //прилагательное
            "ADV", //наречие
            "ADVPRO", //местоименное наречие
            "ANUM", //числительное-прилагательное
            "APRO", //местоимение-прилагательное
            "COM", //часть композита - сложного слова
            "CONJ", //союз
            "INTJ", //междометие
            "NUM", //числительное
            "PART", //частица
            "PR", //предлог
            "SPRO", //местоимение-существительное
            "V" //глагол
        };

        public string GetPrimaryFormOfNouns()
        {
            if (Analysis == null || Text.Length < 2 ||
                Analysis.Any(a =>
                    overlookedPartsOfSpeech.Any(p => a.Grammar.Contains(p))))
                return null;
            foreach (var wordInfo in Analysis)
            {
                if (wordInfo.Grammar.Contains("S") && wordInfo.Grammar.Contains("ед") &&
                    wordInfo.Grammar.Contains("им"))
                    return wordInfo.LexicalForm.ToLower();
            }

            return null;
        }
    }
}