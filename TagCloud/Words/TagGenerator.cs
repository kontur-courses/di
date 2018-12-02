using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.TagCloudVisualization.Layouter;

namespace TagCloud.Words
{
    public class TagGenerator
    {
        private readonly IWordFilter wordFilter;
        private readonly ITagCloudLayouter tagLayouter;
        
        public TagGenerator(IWordFilter wordFilter, TagCloudLayouter tagLayouter)
        {
            this.wordFilter = wordFilter;
            this.tagLayouter = tagLayouter;
        }
        
        public List<Tag> GetTags(IEnumerable<string> words)
        {
            var filteredWords = wordFilter.Filter(words);
            var weightedWords = WordAnalyzer.WeightWords(filteredWords);
            return tagLayouter.GetTags(weightedWords).ToList();
        }
    }
} 