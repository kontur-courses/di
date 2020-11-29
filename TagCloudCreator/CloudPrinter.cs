using System;
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

        private static Bitmap DrawCloud(IEnumerable<DrawingWord> words, Size imageSize)
        {
            var cloud = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(cloud);
            var random = new Random();
            foreach (var word in words)
                graphics.DrawString(word.Word, word.Font,
                    new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255))),
                    word.Location);
            return cloud;
        }

        public Bitmap DrawCloud(string pathToWordsFile, BaseCloudLayouter layouter, Size imageSize,
            FontFamily fontFamily)
        {
            layouter.ClearLayout();
            if (!File.Exists(pathToWordsFile))
                throw new FileNotFoundException();
            var ext = Path.GetExtension(pathToWordsFile);
            var reader = readers.First(x => x.Types.Contains(ext));
            var interestingWords = WordPrepairer.GetInterestingWords(reader.ReadAllLinesFromFile(pathToWordsFile));
            var statistic = WordPrepairer.GetWordsStatistic(interestingWords);
            return DrawCloud(RectanglesForWordsCreator.GetReadyWords(statistic, layouter, fontFamily), imageSize);
        }
    }
}