using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DeepMorphy;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Algorithm
{
    public class WordsCounter : IWordsCounter
    {
        private readonly MorphAnalyzer morph;
        private readonly IParser parser;

        public WordsCounter(MorphAnalyzer morph, IParser parser)
        {
            this.morph = morph;
            this.parser = parser;
        }

        public Dictionary<string, int> CountWords(string pathToSource, string pathToCustomBoringWords)
        {
            var words = parser.CountWordsInFile(pathToSource);
            var customBoringWords = parser.FindWordsInFile(pathToCustomBoringWords);
            var notBoringTypes = new[] { "сущ", "прил", "гл" };

            return words
                .Where(pair =>
                    !customBoringWords.Contains(pair.Key) && 
                    notBoringTypes.Any(type =>
                        morph.GetGrams(pair.Key)["чр"].Contains(type)))
                .OrderByDescending(pair => pair.Value)
                .ToDictionary(e => e.Key, e => e.Value);

        }
    }
}
