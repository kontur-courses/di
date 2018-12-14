using System.Collections.Generic;
using System.Drawing;
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
        private readonly IEnumerable<IWordsProcessor> processors;
        private readonly IWordsCounter counter;
        private readonly IWordsLayouter wordsLayouter;
        private readonly IWordsDrawer wordsDrawer;
        private readonly IEnumerable<IImageSaver> savers;

        public TagCloudGenerator(
            IWordsLayouter wordsLayouter,
            IWordsDrawer wordsDrawer,
            IWordsFileReader wordsFileReader,
            IEnumerable<IWordsProcessor> processors, 
            IWordsCounter counter,
            IEnumerable<IImageSaver> savers)
        {
            this.wordsLayouter = wordsLayouter;
            this.wordsDrawer = wordsDrawer;
            this.wordsFileReader = wordsFileReader;
            this.processors = processors;
            this.counter = counter;
            this.savers = savers;
        }

        public void Generate(Arguments arguments)
        {
            var words = wordsFileReader.Read(arguments.WordsFileName);
            var wordsColor = Color.FromName(arguments.WordsColorName);
            var backgroundColor = Color.FromName(arguments.BackgroundColorName);
            var font = new FontFamily(arguments.FontFamilyName);

            foreach (var wordsProcessor in processors)
                words = wordsProcessor.Process(words);

            var wordInfos = counter.GetWordsInfo(words);
            var layout = wordsLayouter.GenerateLayout(wordInfos, font, arguments.Multiplier);
            var image = wordsDrawer.CreateImage(layout, wordsColor, backgroundColor);

            foreach (var saver in savers)
                saver.Save(image, arguments.ImageFileName);
        }
    }
}