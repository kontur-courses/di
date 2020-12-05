using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CloudLayouters;

namespace TagCloudCreator
{
    public class CloudPrinter
    {
        private readonly IFileReader[] readers;


        public CloudPrinter(IFileReader[] readers)
        {
            this.readers = readers;
        }

        private static Bitmap DrawCloud(IEnumerable<DrawingWord> words, Size imageSize, IColorSelector colorSelector,
            IWordPainter wordPainter)
        {
            var cloud = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(cloud);
            foreach (var word in words)
                graphics.DrawString(word.Word, word.Font,
                    new SolidBrush(colorSelector.GetColor(word)),
                    word.Location);
            return cloud;
        }

        public Bitmap DrawCloud(string pathToWordsFile, BaseCloudLayouter layouter, Size imageSize,
            FontFamily fontFamily, IColorSelector colorSelector, IWordPainter? wordPainter = null)
        {
            wordPainter ??= new SimpleWordPainter();
            layouter.ClearLayout();
            if (!File.Exists(pathToWordsFile))
                throw new FileNotFoundException();
            var ext = Path.GetExtension(pathToWordsFile);
            var reader = readers.First(x => x.Types.Contains(ext));
            var interestingWords = WordPreparer.GetInterestingWords(reader.ReadAllLinesFromFile(pathToWordsFile));
            var statistic = WordPreparer.GetWordsStatistic(interestingWords);
            return DrawCloud(RectanglesForWordsCreator.GetReadyWords(statistic, layouter, fontFamily, wordPainter),
                imageSize,
                colorSelector, wordPainter);
        }
    }
}