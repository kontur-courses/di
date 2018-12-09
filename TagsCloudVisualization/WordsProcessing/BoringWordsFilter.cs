using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class BoringWordsFilter : IFilter
    {
        private readonly IEnumerable<string> boringWords;
        
        public BoringWordsFilter(IWordsProvider boringWordsProvider)
        {
            this.boringWords = boringWordsProvider.Provide();
        }


        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}