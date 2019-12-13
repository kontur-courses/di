using System.Collections.Generic;
using NHunspell;

namespace TagsCloudContainer.Word_Counting
{
    public class WordNormalizer : IWordNormalizer
    {
        private readonly Hunspell hunspell;

        public WordNormalizer(Hunspell hunspell)
        {
            this.hunspell = hunspell;
        }

        public string Normalize(string word)
        {
            var lowered = word.ToLower();
            var stemmed = hunspell.Stem(lowered);
            return stemmed.Count != 0
                ? hunspell.Stem(word.ToLower())[0]
                : lowered;
        }
    }
}