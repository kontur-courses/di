using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.WordConverters
{
    public class NormalFormWordConverter : IWordConverter
    {
        private readonly string affPath;
        private readonly string dicPath;

        public NormalFormWordConverter(string affPath, string dicPath)
        {
            this.affPath = affPath;
            this.dicPath = dicPath;
        }
        
        public IEnumerable<string> ConvertWords(IEnumerable<string> words)
        {
            var normalFormWords = new List<string>();
            using (var hunspell = new Hunspell(affPath, dicPath))
            {
                foreach (var word in words)
                {
                    var normalForms = hunspell.Stem(word);
                    normalFormWords.Add( normalForms.Count > 0 ? normalForms.First() : word);
                }
            }

            return normalFormWords;
        }
    }
}