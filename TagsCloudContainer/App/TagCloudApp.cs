using System.Linq;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.CounterNamespace;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.MorphologicalAnalysis;
using TagsCloudContainer.Visualizer;

namespace TagsCloudContainer.App
{
    public class TagCloudApp : IApp
    {
        private readonly MorphologicalAnalyzer analyzer;
        private readonly ICloudLayouter cloud;
        private readonly Options options;
        private readonly IVisualizer visualizer;

        public TagCloudApp(ICloudLayouter cloud, IVisualizer visualizer,
            MorphologicalAnalyzer analyzer, Options options)
        {
            this.options = options;
            this.cloud = cloud;
            this.visualizer = visualizer;
            this.analyzer = analyzer;
        }

        public void Run()
        {
            FillCloud();
            visualizer.Visualize();
            visualizer.Save();
        }

        private Counter<Word> GetCounterSelectedWords()
        {
            var partSpeech = MorphologicalAnalyzer.GetPartSpeech(options.PartSpeeches);
            var words = analyzer.GetWords().Where(word => partSpeech.HasFlag(word.PartSpeech));
            return new Counter<Word>(words);
        }

        private void FillCloud()
        {
            var counter = GetCounterSelectedWords();
            var words = counter.GetMostPopular(options.Count).ToList();
            var minAmount = counter.GetAmount(words.Last());
            var maxAmount = counter.GetAmount(words.First());
            foreach (var word in words)
                if (visualizer is TagCloudVisualizer tagCloudVisualizer)
                {
                    var font = tagCloudVisualizer
                        .GetFontByWeightWord(counter.GetAmount(word), minAmount, maxAmount);
                    var size = tagCloudVisualizer.MeasureString(word.Text, font).Ceiling();
                    cloud.PutNextCloudItem(word.Text, size, font);
                }
        }
    }
}