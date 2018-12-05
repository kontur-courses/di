using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class TagsCloud
    {
        public void Generate(
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileReader fileReader,
            IColorScheme colorScheme,
            ISizeScheme sizeScheme,
            IFontScheme fontScheme,
            IStatisticsCollector statisticsCollector,
            IWordExcluder wordExcluder)
        {
            var input = fileReader.ReadLines();
            var filteredInput = ExcludeWords(input, wordExcluder);
            var statistics = statisticsCollector.GetStatistics(filteredInput);
            var fontedElements = ApplyFontScheme(statistics, fontScheme);
            var positionedElements = FillCloud(fontedElements, layouter, sizeScheme);
            var visualElements = ApplyColorSchemes(positionedElements, colorScheme, fontScheme);
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
            IColorScheme colorScheme, 
            IFontScheme fontScheme)
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