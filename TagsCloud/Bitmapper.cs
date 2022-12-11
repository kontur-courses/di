using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class Bitmapper : IBitmapper
    {
        private readonly Bitmap bitmap;
        private Graphics graphics => Graphics.FromImage(bitmap);
        
        private readonly ICloudLayouter cloudLayouter;
        private readonly IPrintSettings printSettings;

        public Bitmapper(IPrintSettings printSettings, ICloudLayouter cloudLayouter)
        {
            bitmap = new Bitmap(printSettings.Width, printSettings.Height);

            this.cloudLayouter = cloudLayouter;
            this.printSettings = printSettings;
        }

        private void FillBackground()
        {
            using (var solidBrush = new SolidBrush(printSettings.Background))
            {
                graphics.FillRectangle(solidBrush, 0, 0, bitmap.Width, bitmap.Height);
            }
        }

        private void DrawSingleWord(Pen pen, KeyValuePair<double, List<string>> words)
        {
            foreach (var word in words.Value)
            {
                var fontSize = (int)(words.Key * printSettings.FontSize);
                var font = new Font(printSettings.FontName, fontSize);

                var wordSizeF = graphics.MeasureString(word, font);
                var wordSize = new Size((int)wordSizeF.Width + 2, (int)wordSizeF.Height + 2);

                var newWordLocation = cloudLayouter.PutNextRectangle(wordSize);

                using (var solidBrush = new SolidBrush(pen.Color))
                {
                    graphics.DrawString(word, font, solidBrush, newWordLocation);
                }
            }
        }

        public void DrawWords(SortedDictionary<double, List<string>> words)
        {
            FillBackground();

            var ind = 0;

            foreach (var word in words)
            {
                if (ind == 0)
                {
                    DrawSingleWord(printSettings.CentralPen, word);
                }
                else
                {
                    DrawSingleWord(printSettings.SurroundPen, word);
                }

                ind++;
            }
        }

        public void SaveFile(string directory)
        {
            bitmap.Save(directory);
        }
    }
}
