using System.Collections.Generic;
using System.Linq;
using TagCloud.Filters;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.TagCloudVisualization.Layouter;

namespace TagCloud.Words
{
    public class TagGenerator : ITagGenerator
    {
        private readonly FilterManager filterManager;
        private readonly ITagCloudLayouter tagLayouter;
        private IWordAnalyzer wordAnalyzer;

        public TagGenerator(FilterManager filterManager, ITagCloudLayouter tagLayouter, IWordAnalyzer wordAnalyzer)
        {
            this.filterManager = filterManager;
            this.tagLayouter = tagLayouter;
            this.wordAnalyzer = wordAnalyzer;
        }
        
        public List<Tag> GetTags(IEnumerable<string> words)
        {
            var filteredWords = filterManager.ApplyFilters(words);
            var weightedWords = wordAnalyzer.WeightWords(filteredWords);
            return tagLayouter.GetCloudTags(weightedWords).ToList();
        }
    }
} 