using TagCloud.Counter;
using TagCloud.Data;
using TagCloud.Drawer;
using TagCloud.Processor;
using TagCloud.Reader;
using TagCloud.Saver;
using TagCloud.WordsLayouter;

namespace TagCloud
{
    public class TagCloudGenerator
    {
        private readonly IWordsFileReader wordsFileReader;
        private readonly IWordsFileReader boringWordsFileReader;
        private readonly IWordsProcessor processor;
        private readonly IWordsCounter counter;
        private readonly IWordsLayouter wordsLayouter;
        private readonly IWordsDrawer wordsDrawer;
        private readonly IImageSaver saver;

        public TagCloudGenerator(
            IWordsLayouter wordsLayouter,
            IWordsDrawer wordsDrawer,
            IWordsFileReader wordsFileReader,
            IWordsFileReader boringWordsFileReader,
            IWordsProcessor processor, 
            IWordsCounter counter,
            IImageSaver saver)
        {
            this.wordsLayouter = wordsLayouter;
            this.wordsDrawer = wordsDrawer;
            this.wordsFileReader = wordsFileReader;
            this.boringWordsFileReader = boringWordsFileReader;
            this.processor = processor;
            this.counter = counter;
            this.saver = saver;
        }

        public void Generate(Arguments arguments)
        {
            var words = wordsFileReader.Read(arguments.WordsFileName);

            var wordInfos = counter.GetWordsInfo(processor.Process(words));
            var layout = wordsLayouter.GenerateLayout(wordInfos, arguments.FontFamily, arguments.Multiplier);
            var image = wordsDrawer.CreateImage(layout, arguments.WordsColor, arguments.BackgroundColor);
            saver.Save(image, arguments.ImageFileName);
        }
    }
}