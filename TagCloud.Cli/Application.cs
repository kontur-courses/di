using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;

namespace TagCloudCreator
{
    public class Application
    {
        private readonly IFileReader fileReader;
        private readonly IImageSaver imageSaver;
        private readonly ICloudLayouter layouter;
        private readonly ISizeScheme sizeScheme;
        private readonly IStatisticsCollector statisticsCollector;
        private readonly IVisualizer visualizer;
        private readonly IWordFilter wordFilter;
        private readonly IWordProcessor wordProcessor;

        public Application(
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileReader fileReader,
            IImageSaver imageSaver,
            IStatisticsCollector statisticsCollector,
            IWordFilter wordFilter,
            ISizeScheme sizeScheme,
            IWordProcessor wordProcessor)
        {
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.fileReader = fileReader;
            this.imageSaver = imageSaver;
            this.statisticsCollector = statisticsCollector;
            this.wordFilter = wordFilter;
            this.sizeScheme = sizeScheme;
            this.wordProcessor = wordProcessor;
        }

        public void Run(string inputFile, string outputFile)
        {
            var input = fileReader.Read(inputFile);
            input = input.Select(s => s.ToLower());
            input = ProcessWords(input, wordProcessor);
            var filteredInput = ExcludeWords(input, wordFilter);
            var statistics = statisticsCollector.GetStatistics(filteredInput);
            var positionedElements = FillCloud(statistics);
            var image = visualizer.Visualize(positionedElements);
            imageSaver.Save(image, outputFile);
        }

        private IEnumerable<string> ProcessWords(IEnumerable<string> words, IWordProcessor processor)
        {
            var result = new List<string>();
            foreach (var word in words)
            {
                var processed = processor.Process(word);
                result.Add(processed ?? word);
            }

            return result;
        }

        private IEnumerable<string> ExcludeWords(IEnumerable<string> words, IWordFilter filter)
        {
            return words.Where(w => !filter.ToExclude(w)).ToArray();
        }

        private IEnumerable<PositionedElement> FillCloud(
            IEnumerable<FrequentedWord> statistics)
        {
            var elements = new List<PositionedElement>();
            foreach (var word in statistics)
            {
                var size = sizeScheme.GetSize(word);
                var rectangle = layouter.PutNextRectangle(size);
                var element = new PositionedElement(word, rectangle);
                elements.Add(element);
            }

            return elements;
        }
    }
}