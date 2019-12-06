using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Visualization.Interfaces;
using TagsCloudContainer.Word_Counting;

namespace TagsCloudContainer
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly IFileParser parser;
        private readonly IWordCounter wordCounter;
        private readonly ISizeTranslator translator;
        private readonly IVisualizer visualizer;
        private readonly IWordLayouter layouter;


        public CloudLayouter(IFileParser parser, IWordCounter wordCounter, ISizeTranslator translator,
            IVisualizer visualizer, IWordLayouter layouter)
        {
            this.parser = parser;
            this.wordCounter = wordCounter;
            this.translator = translator;
            this.visualizer = visualizer;
            this.layouter = layouter;
        }

        public void Layout(string inputPath, string outputPath)
        {
            var parsed = parser.ParseFile(inputPath);
            var wordCount = wordCounter.CountWords(parsed);
            var rectangles = layouter.LayoutWords(translator.TranslateWordsToSizedWords(wordCount));
            visualizer.Visualize(rectangles, outputPath);
        }
    }
}