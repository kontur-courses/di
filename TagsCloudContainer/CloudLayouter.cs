using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Word_Counting;

namespace TagsCloudContainer
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly IFileParser parser;
        private readonly IWordCounter wordCounter;
        private readonly IRectangleTranslator translator;
        private readonly IVisualizer visualizer;

        public CloudLayouter(IFileParser parser, IWordCounter wordCounter, IRectangleTranslator translator, IVisualizer visualizer)
        {
            this.parser = parser;
            this.wordCounter = wordCounter;
            this.translator = translator;
            this.visualizer = visualizer;
        }

        public void Layout(string filePath)
        {
            var parsed = parser.ParseFile(filePath);
            var wordCount = wordCounter.CountWords(parsed);
            var rectangles = translator.TranslateWordsToRectangles(wordCount);
            visualizer.Visualize(rectangles);
        }
    }
}