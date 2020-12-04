using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;
using YandexMystem.Wrapper.Models;

namespace TagCloud.WordsAnalyzer.WordFilters
{
    public class AllowedGramPartsFilter : IWordFilter
    {
        private Regex cyrillicRegex;
        private Mysteam mysteam;

        private HashSet<GramPartsEnum> allowedGramParts = new HashSet<GramPartsEnum>();
        
        public AllowedGramPartsFilter(IEnumerable<string> gramPartsToAllow)
        {
            MakeAllowedGramPartsHashSet(gramPartsToAllow);
            cyrillicRegex = new Regex("\\p{IsCyrillic}");
            mysteam = new Mysteam();
        }
        
        public bool ShouldExclude(string word)
        {
            if (!IsCyrillicWord(word))
                return false;
            var models = mysteam.GetWords(word);
            if (models.Count != 1)
                return false;

            var model = models[0];
            return !allowedGramParts.Contains(model.Lexems[0].GramPart);
        }

        private void MakeAllowedGramPartsHashSet(IEnumerable<string> gramPartsToAllow)
        {
            foreach (var gramPart in gramPartsToAllow)
            {
                switch (gramPart)
                {
                    case "Noun":
                        allowedGramParts.Add(GramPartsEnum.Noun);
                        break;
                    case "Verb":
                        allowedGramParts.Add(GramPartsEnum.Verb);
                        break;
                    case "Adjective":
                        allowedGramParts.Add(GramPartsEnum.Adjective);
                        break;
                    case "Adverb":
                        allowedGramParts.Add(GramPartsEnum.Adverb);
                        break;
                    case "Conjunction":
                        allowedGramParts.Add(GramPartsEnum.Conjunction);
                        break;
                }
            }
        }
        
        private bool IsCyrillicWord(string word)
        {
            return cyrillicRegex.IsMatch(word);
        }
    }
}