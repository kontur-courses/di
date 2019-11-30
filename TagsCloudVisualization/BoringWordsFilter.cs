using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class BoringWordsFilter : ITextFilter
    {
        private readonly IEnumerable<string> boringWords;

        public BoringWordsFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords;
        }
        
        public BoringWordsFilter()
        {
            // TODO добавить дефолтный словарь скучных слов
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}