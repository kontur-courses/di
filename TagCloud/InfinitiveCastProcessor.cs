using System.Linq;
using JetBrains.Annotations;
using NHunspell;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class InfinitiveCastProcessor : IWordProcessor
    {
        private readonly Hunspell hunspell;

        public InfinitiveCastProcessor(byte[] affixFileData, byte[] dictionaryFileData)
        {
            hunspell = new Hunspell(affixFileData, dictionaryFileData);
        }

        [CanBeNull]
        public string Process(string word)
        {
            return hunspell.Stem(word).FirstOrDefault();
        }
    }
}