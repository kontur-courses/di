using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization
{
    public class BoringWordsCleaner : IWordsCleaner
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsCleaner(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }

        public List<string> CleanWords(List<string> words)
        {
            var ruDict = Path.Join(Directory.GetCurrentDirectory(), "ru_RU.dic");
            var ruAff = Path.Join(Directory.GetCurrentDirectory(), "ru_RU.aff");
            var hunspell = new Hunspell(ruAff, ruDict);
            return words.Select(word => hunspell.Stem(word.ToLower()).FirstOrDefault())
                .Where(loweredWord => !boringWords.Contains(loweredWord))
                .ToList();
        }
    }
}