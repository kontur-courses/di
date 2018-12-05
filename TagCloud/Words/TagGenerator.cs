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
        private readonly WordManager wordManager;
        private readonly ITagCloudLayouter tagLayouter;
        private IWordAnalyzer wordAnalyzer;

        public TagGenerator(WordManager wordManager, ITagCloudLayouter tagLayouter, IWordAnalyzer wordAnalyzer)
        {
            this.wordManager = wordManager;
            this.tagLayouter = tagLayouter;
            this.wordAnalyzer = wordAnalyzer;
        }
        
        public List<Tag> GetTags(IEnumerable<string> words)
        {
            var filteredWords = wordManager.ApplyFilters(words);
            var weightedWords = wordAnalyzer.WeightWords(filteredWords);
            return tagLayouter.GetCloudTags(weightedWords).ToList();
        }
    }
} 