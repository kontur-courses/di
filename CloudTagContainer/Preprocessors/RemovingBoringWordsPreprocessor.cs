using System;
using System.Linq;
using WeCantSpell.Hunspell;

namespace Visualization.Preprocessors
{
    public class RemovingBoringWordsPreprocessor : IWordsPreprocessor
    {
        private readonly IHunspeller hunspeller;

        public RemovingBoringWordsPreprocessor(IHunspeller hunspeller)
        {
            this.hunspeller = hunspeller;
        }

        public string[] Preprocess(string[] rawWords)
        {
            return rawWords
                .Where(word => hunspeller.Check(word))
                .ToArray();
        }
    }
}