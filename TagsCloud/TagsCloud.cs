using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class TagsCloud
    {
        private ICloudLayouter layouter;
        private IVisualizer visualizer;
        private IFileReader fileReader;
        private IColorScheme colorScheme;
        private ISizeScheme sizeScheme;
        private IFontScheme fontScheme;
        private IStatisticsCollector statisticsCollector;
        private IWordExcluder wordExcluder;

        public TagsCloud(
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileReader fileReader,
            IColorScheme colorScheme,
            ISizeScheme sizeScheme,
            IFontScheme fontScheme,
            IStatisticsCollector statisticsCollector,
            IWordExcluder wordExcluder)
        {
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.fileReader = fileReader;
            this.colorScheme = colorScheme;
            this.sizeScheme = sizeScheme;
            this.fontScheme = fontScheme;
            this.statisticsCollector = statisticsCollector;
            this.wordExcluder = wordExcluder;
        }

        public void Generate(string file)
        {
            fileReader.Path = file;
            var input = fileReader.Read();
            input = input.Select(s => s.ToLower());
            var filteredInput = ExcludeWords(input, wordExcluder);
            var statistics = statisticsCollector.GetStatistics(filteredInput);
            var fontedElements = ApplyFontScheme(statistics, fontScheme);
            var positionedElements = FillCloud(fontedElements, layouter, sizeScheme);
            var visualElements = ApplyColorSchemes(positionedElements, colorScheme);
            visualizer.Visualize(visualElements);
        }

        private IEnumerable<FrequentedFontedWord> ApplyFontScheme(
            IEnumerable<FrequentedWord> elements,
            IFontScheme fontScheme)
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
            IEnumerable<PositionedElement> elements,
            IColorScheme colorScheme)
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

        private IEnumerable<string> ExcludeWords(IEnumerable<string> words, IWordExcluder excluder)
            => words.Where(w => !excluder.ToExclude(w));

        private IEnumerable<PositionedElement> FillCloud(
            IEnumerable<FrequentedFontedWord> statistics, 
            ICloudLayouter layouter,
            ISizeScheme sizeScheme)
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