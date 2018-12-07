using System.Collections.Generic;
using System.Drawing;
using TagCloud.Counter;
using TagCloud.Data;
using TagCloud.Drawer;
using TagCloud.Processor;
using TagCloud.Reader;
using TagCloud.Validator;
using TagCloud.WordsLayouter;

namespace TagCloud
{
    public class TagCloudGenerator
    {
        private readonly IWordsFileReader wordsFileReader;
        private readonly IWordsFileReader boringWordsFileReader;
        private readonly IWordsValidator validator;
        private readonly IWordsProcessor processor;
        private readonly IWordsCounter counter;
        private readonly IWordsLayouter wordsLayouter;
        private readonly IWordsDrawer wordsDrawer;

        public TagCloudGenerator(
            IWordsLayouter wordsLayouter,
            IWordsDrawer wordsDrawer,
            IWordsFileReader wordsFileReader,
            IWordsFileReader boringWordsFileReader,
            IWordsValidator validator, 
            IWordsProcessor processor, 
            IWordsCounter counter)
        {
            this.wordsLayouter = wordsLayouter;
            this.wordsDrawer = wordsDrawer;
            this.wordsFileReader = wordsFileReader;
            this.boringWordsFileReader = boringWordsFileReader;
            this.validator = validator;
            this.processor = processor;
            this.counter = counter;
        }

        public Bitmap Generate(Arguments arguments)
        {
            var words = wordsFileReader.Read(arguments.WordsFileName);
            var boringWords = boringWordsFileReader.Read(arguments.BoringWordsFileName);

            var wordInfos = counter.Count(processor.Process(validator.Validate(words, boringWords)));
            var layout = wordsLayouter.GenerateLayout(wordInfos, arguments.FontFamily, arguments.Multiplier);
            var image = wordsDrawer.CreateImage(layout, arguments.WordsBrush, arguments.BackgroundBrush);
            return image;
        }
    }
}