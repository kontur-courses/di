using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloudContainer
{
    public class TagCloud : ITagCloud
    {
        private readonly IFilterApplyer filterApplyer;
        private readonly IWordsFrequencyAnalyzer frequencyAnalyzer;
        private readonly IVisualizer visualizer;
        private readonly IWordsConverter wordsConverter;


        public TagCloud(IFilterApplyer filterApplyer,
            IWordsFrequencyAnalyzer frequencyAnalyzer,
            IVisualizer visualizer,
            IWordsConverter wordsConverter)
        {
            this.filterApplyer = filterApplyer;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.visualizer = visualizer;
            this.wordsConverter = wordsConverter;
        }

        public Bitmap LayDown(IEnumerable<string> words, ITagCloudSettings settings)
        {
            var convertedWords = wordsConverter.Convert(words);
            var filteredWords = filterApplyer.Apply(convertedWords);
            var freqDict = frequencyAnalyzer.GetWordsFrequency(filteredWords);
            return visualizer.Visualize(freqDict);
        }
    }
}