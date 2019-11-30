using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var font = new Font("Arial", 10, FontStyle.Bold);
            var pictureSize = new Size(500, 500);
            var layouter = new CircularCloudLayouter(new Point(250, 250));
            var words = new TextReader("2.txt").GetWords();
            var preprocessedWords = words.GetLowerCaseWords().GetFilteredWords(new ShortWordsFilter(3));
            var rectangles = preprocessedWords
                .Select(word => layouter.PutNextRectangle(word.GetSize(font, pictureSize))).ToList();
            var painter = new Painter(pictureSize);
            var image = painter.GetMulticolorTagCloud(preprocessedWords, rectangles, font);
            ImageSaver.SaveImageToDefaultDirectory("example", image);
        }
    }
}