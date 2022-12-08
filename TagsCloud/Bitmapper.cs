using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class Bitmapper
    {
        private CircularCloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics => Graphics.FromImage(bitmap);

        private readonly PrintSettings printSettings;

        public Bitmapper(int width, int height, PrintSettings printSettings)
        {
            bitmap = new Bitmap(width, height);
            layouter = new CircularCloudLayouter(new Point((int)(width / 2), (int)(height / 2)));

            this.printSettings = printSettings;
        }

        private void FillBackground()
        {
            using (var solidBrush = new SolidBrush(printSettings.Background))
            {
                graphics.FillRectangle(solidBrush, 0, 0, bitmap.Width, bitmap.Height);
            }
        }

        private void DrawSingleWord(Pen pen, KeyValuePair<string, double> word)
        {
            var fontSize = (int)(word.Value * printSettings.FontSize);
            var font = new Font(printSettings.FontName, fontSize);

            var wordSizeF = graphics.MeasureString(word.Key, font);
            var wordSize = new Size((int)wordSizeF.Width + 1, (int)wordSizeF.Height + 1);

            var newWordLocation = layouter.PutNextRectangle(wordSize);

            using (var solidBrush = new SolidBrush(pen.Color))
            {
                graphics.DrawString(word.Key, font, solidBrush, newWordLocation);
            }
        }

        private void ClearFrame()
        {
            bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            layouter = new CircularCloudLayouter(new Point((int)(bitmap.Width / 2), (int)(bitmap.Height / 2)));
        }

        public void DrawWords(Dictionary<string, double> words)
        {
            ClearFrame();
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
