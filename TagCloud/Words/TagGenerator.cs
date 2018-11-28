using System.Collections.Generic;
using System.Linq;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.TagCloudVisualization.Layouter;

namespace TagCloud.Words
{
    public class TagGenerator
    {
        private readonly WordFilter wordFilter;
        private readonly TagCloudLayouter tagLayouter;
        
        public TagGenerator(WordFilter wordFilter, TagCloudLayouter tagLayouter)
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