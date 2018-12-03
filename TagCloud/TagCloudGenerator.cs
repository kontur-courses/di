using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Hosting;
using TagCloud.Data;
using TagCloud.Drawer;
using TagCloud.Parser;
using TagCloud.WordsLayouter;

namespace TagCloud
{
    public class TagCloudGenerator
    {
        private readonly IWordsParser wordsParser;
        private readonly IWordsLayouter wordsLayouter;
        private readonly IWordsDrawer wordsDrawer;

        public TagCloudGenerator(IWordsParser wordsParser, IWordsLayouter wordsLayouter, IWordsDrawer wordsDrawer)
        {
            this.wordsParser = wordsParser;
            this.wordsLayouter = wordsLayouter;
            this.wordsDrawer = wordsDrawer;
        }

        public Bitmap Generate(IEnumerable<string> words, IEnumerable<string> boringWords, Arguments arguments)
        {
            var wordInfos = wordsParser.Parse(words, new HashSet<string>(boringWords));
            var layout = wordsLayouter.GenerateLayout(wordInfos, arguments.FontFamily, arguments.Multiplier);
            var image = wordsDrawer.CreateImage(layout, arguments.WordsBrush, arguments.BackgroundBrush);
            return image;
        }
    }
}