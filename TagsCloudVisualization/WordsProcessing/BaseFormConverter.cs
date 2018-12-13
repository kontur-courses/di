using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.WordsProcessing
{
    public class BaseFormConverter : IWordsChanger
    {
        private readonly string affFileName;
        private readonly string dicFileName;

        public BaseFormConverter(string affFileName, string dicFileName)
        {
            this.affFileName = affFileName;
            this.dicFileName = dicFileName;
        }

        public IEnumerable<string> ChangeWords(IEnumerable<string> words)
        {

            using (var hunspell = new Hunspell(affFileName, dicFileName))
            {
                foreach (var word in words)
                {
                    var stems = hunspell.Stem(word);
                    yield return stems.Count != 0 ? stems.First() : word;
                }
            }
        }
    }
}
