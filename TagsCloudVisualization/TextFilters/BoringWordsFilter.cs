using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using TagsCloudVisualization.PathFinders;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;

namespace TagsCloudVisualization.TextFilters
{
    public class BoringWordsFilter : ITextFilter
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords.ToHashSet();
        }
        
        public BoringWordsFilter()
        {
            var text = new TxtReader().ReadText(PathFinder.GetTextsPath("boringWords"), Encoding.Default);
            var words = new WordsExtractor().GetWords(text);
            boringWords = words.ToHashSet();
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}