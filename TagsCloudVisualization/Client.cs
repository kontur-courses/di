using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.Curves;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualization
{
    public class Client
    {
        private readonly IPainter painter;
        private readonly ITextFormatter formatter;
        private readonly ICloudLayouter layouter;

        public Client(ICloudLayouter layouter, IPainter painter, ITextFormatter formatter)
        {
            this.layouter = layouter;
            this.painter = painter;
            this.formatter = formatter;
        }

        public void Draw(string destinationPath, FontFamily font)
        {
            var services = Program.Container.GetServices<IFileReader>();
            foreach (var service in services)
            {
                if (service.TryReadAllText(out var text))
                {
                    var words = MakeRectangles(formatter.Format(text), font);
                    painter.DrawWordsToFile(words, destinationPath);
                    return;
                }
            }

            throw new Exception("IFileReader not found!");
        }


        private List<Word> MakeRectangles(List<Word> words, FontFamily fontFamily)
        {
            var processedWords = new List<Word>();

            double minFrequency = words.Min(x => x.Frequency);

            double maxFrequency = words.Max(x => x.Frequency);

            foreach (var word in words)
            {
                var font = GetWordFontByFrequency(fontFamily, 12, 36, minFrequency, maxFrequency, word.Frequency);

                var rectangleSize = GetWordLayoutRectangleSize(word.Value, font);

                var rectangle = layouter.PutNextRectangle(rectangleSize);

                word.Font = font;
                word.Rectangle = rectangle;
                processedWords.Add(word);

            }
            return processedWords;
        }

        private Font GetWordFontByFrequency(FontFamily fontFamily, int minFontSize, int maxFontSize, double minFrequency, double maxFrequency, double wordFrequency)
        {
            var fontSize = (int)(minFontSize + (maxFontSize - minFontSize) * (wordFrequency - minFrequency) / (maxFrequency - minFrequency));

            return new Font(fontFamily, fontSize);
        }

        private Size GetWordLayoutRectangleSize(string word, Font font)
        {
            Image fakeImage = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(fakeImage);
            var wordSize = graphics.MeasureString(word, font);
            var width = (int)Math.Ceiling(wordSize.Width);
            var height = (int)Math.Ceiling(wordSize.Height);
            return new Size(width, height);
        }
    }
}
