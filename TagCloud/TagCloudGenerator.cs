using System.Collections.Generic;
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
        private readonly IWordsProcessor processor;
        private readonly IWordsCounter counter;
        private readonly IWordsLayouter wordsLayouter;
        private readonly IWordsDrawer wordsDrawer;
        private readonly IEnumerable<IImageSaver> savers;

        public TagCloudGenerator(
            IWordsLayouter wordsLayouter,
            IWordsDrawer wordsDrawer,
            IWordsFileReader wordsFileReader,
            IWordsProcessor processor, 
            IWordsCounter counter,
            IEnumerable<IImageSaver> savers)
        {
            this.wordsLayouter = wordsLayouter;
            this.wordsDrawer = wordsDrawer;
            this.wordsFileReader = wordsFileReader;
            this.processor = processor;
            this.counter = counter;
            this.savers = savers;
        }

        public void Generate(Arguments arguments)
        {
            var words = wordsFileReader.Read(arguments.WordsFileName);

            var wordInfos = counter.GetWordsInfo(processor.Process(words));
            var layout = wordsLayouter.GenerateLayout(wordInfos, arguments.FontFamily, arguments.Multiplier);
            var image = wordsDrawer.CreateImage(layout, arguments.WordsColor, arguments.BackgroundColor);

            foreach (var saver in savers)
                saver.Save(image, arguments.ImageFileName);
        }
    }
}