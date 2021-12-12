using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.Common.Stemers
{
    public class HunspellStemer : IStemer
    {
        private readonly Hunspell hunspellDict;

        public HunspellStemer(Hunspell hunspellDict)
        {
            this.hunspellDict = hunspellDict;
        }

        public bool TryGetStem(string word, out string stem)
        {
            stem = hunspellDict.Stem(word.ToLower()).FirstOrDefault();
            return stem != null;
        }
    }
}