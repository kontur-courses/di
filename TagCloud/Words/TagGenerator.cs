using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.TagCloudVisualization.Layouter;

namespace TagCloud.Words
{
    public class TagGenerator : ITagGenerator
    {
        private readonly IWordFilter wordFilter;
        private readonly ITagCloudLayouter tagLayouter;
        private IWordAnalyzer wordAnalyzer;

        public TagGenerator(IWordFilter wordFilter, ITagCloudLayouter tagLayouter, IWordAnalyzer wordAnalyzer)
        {
            this.wordFilter = wordFilter;
            this.tagLayouter = tagLayouter;
            this.wordAnalyzer = wordAnalyzer;
        }
        
        public List<Tag> GetTags(IEnumerable<string> words)
        {
            var filteredWords = wordFilter.Filter(words);
            var weightedWords = wordAnalyzer.WeightWords(filteredWords);
            return tagLayouter.GetCloudTags(weightedWords).ToList();
        }
    }
} 