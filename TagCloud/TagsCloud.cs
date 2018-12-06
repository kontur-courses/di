using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.Visualizer;

namespace TagCloud
{
    public class TagsCloud
    {
        private readonly IColorScheme colorScheme;
        private readonly IFileReader fileReader;
        private readonly IFontScheme fontScheme;
        private readonly ICloudLayouter layouter;
        private readonly ISizeScheme sizeScheme;
        private readonly IStatisticsCollector statisticsCollector;
        private readonly IVisualizer visualizer;
        private readonly IWordFilter wordFilter;

        public TagsCloud(
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileReader fileReader,
            IColorScheme colorScheme,
            ISizeScheme sizeScheme,
            IFontScheme fontScheme,
            IStatisticsCollector statisticsCollector,
            IWordFilter wordFilter)
        {
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.fileReader = fileReader;
            this.colorScheme = colorScheme;
            this.sizeScheme = sizeScheme;
            this.fontScheme = fontScheme;
            this.statisticsCollector = statisticsCollector;
            this.wordFilter = wordFilter;
        }

        public void Generate(string file)
        {
            fileReader.Path = file;
            var input = fileReader.Read();
            input = input.Select(s => s.ToLower());
            var filteredInput = ExcludeWords(input, wordFilter);
            var statistics = statisticsCollector.GetStatistics(filteredInput);
            var fontedElements = ApplyFontScheme(statistics);
            var positionedElements = FillCloud(fontedElements);
            var visualElements = ApplyColorSchemes(positionedElements);
            visualizer.Visualize(visualElements);
        }

        private IEnumerable<FrequentedFontedWord> ApplyFontScheme(
            IEnumerable<FrequentedWord> elements)
        {
            var result = new List<FrequentedFontedWord>();
            foreach (var frequentedWord in elements)
            {
                var font = fontScheme.Process(frequentedWord);
                var newElement = new FrequentedFontedWord(frequentedWord.Word, frequentedWord.Frequency, font);
                result.Add(newElement);
            }

            return result;
        }

        private IEnumerable<VisualElement> ApplyColorSchemes(
            IEnumerable<PositionedElement> elements)
        {
            var result = new List<VisualElement>();
            foreach (var element in elements)
            {
                var color = colorScheme.Process(element);
                var visualElement = new VisualElement(element, color);
                result.Add(visualElement);
            }

            return result;
        }

        private IEnumerable<string> ExcludeWords(IEnumerable<string> words, IWordFilter filter)
        {
            return words.Where(w => !filter.ToExclude(w));
        }

        private IEnumerable<PositionedElement> FillCloud(
            IEnumerable<FrequentedFontedWord> statistics)
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