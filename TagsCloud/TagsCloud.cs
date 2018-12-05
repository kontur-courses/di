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
            var positionedElements = FillCloud(statistics, layouter, sizeScheme);
            var visualElements = ApplyColorAndFontSchemes(positionedElements, colorScheme, fontScheme);
            visualizer.Visualize(visualElements);
        }

        private IEnumerable<VisualElement> ApplyColorAndFontSchemes(
            IEnumerable<PositionedElement> elements,
            IColorScheme colorScheme, 
            IFontScheme fontScheme)
        {
            var result = new List<VisualElement>();
            foreach (var element in elements)
            {
                var color = colorScheme.Process(element);
                var font = fontScheme.Process(element);
                var visualElement = new VisualElement(element, color, font);
                result.Add(visualElement);
            }

            return result;
        }

        private IEnumerable<string> ExcludeWords(IEnumerable<string> words, IWordExcluder excluder)
            => words.Where(w => !excluder.ToExclude(w));

        private IEnumerable<PositionedElement> FillCloud(
            IEnumerable<FrequentedWord> statistics, 
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