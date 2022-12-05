using System.Collections.Generic;

namespace TagsCloudVisualisation.WordsPreprocessor.BoringWords
{
    public class BoringUnionsAndAppealsRu : IBoringWords
    {
        private static readonly List<string> Unions = new List<string>
        {
            "и", "или", "а", "с", "при", "но", "однако"
        };
        private static readonly List<string> Prepositions = new List<string>
        {
            "в", "из", "к", "у", "по", "из-за", "по-над", "под", "около", "вокруг", "перед", "возле"
        };

        private static readonly List<string> Appeals = new List<string>
        {
            "он", "она", "оно", "они", "им", "ей", "ему", "её", "его", "их"
        };
        
        public bool IsBoring(Word word)
        {
            var wordValue = word.Value;
            return Unions.Contains(wordValue) || Prepositions.Contains(wordValue) || Appeals.Contains(wordValue);
        }
    }
}