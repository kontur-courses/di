using System;
using System.Collections.Generic;
using System.Drawing;
using CloudLayouter;

namespace CloudDrawing
{
    public class CircularCloudDrawing : ICircularCloudDrawing
    {
        private ICloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics;

        public CircularCloudDrawing(ICloudLayouter cloudLayouter)
        {
            layouter = cloudLayouter;
        }

        public void SetOptions(ImageSettings imageSettings)
        {
            if (imageSettings.Size.Height <= 0 || imageSettings.Size.Height <= 0)
                throw new AggregateException("Size have zero width or height");
            bitmap = new Bitmap(imageSettings.Size.Width, imageSettings.Size.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(imageSettings.Background);
            layouter.SetCenter(new Point(imageSettings.Size.Width / 2, imageSettings.Size.Height / 2));
        }

        public void DrawWords(IEnumerable<(string, int)> wordsFontSize, WordDrawSettings settings)
        {
            foreach (var (word, fontSize) in wordsFontSize)
            {
                var rectangle = DrawWord(word, new Font(settings.FamilyName, fontSize),
                    settings.Brush, settings.StringFormat);
                if (settings.HaveDelineation)
                    DrawRectangle(rectangle);
            }
        }

        private Rectangle DrawWord(string word, Font font, Brush brush, StringFormat stringFormat)
        {
            var stringSize = (graphics.MeasureString(word, font) + new SizeF(1, 1)).ToSize();
            var stringRectangle = layouter.PutNextRectangle(stringSize);
            graphics.DrawString(word, font, brush, stringRectangle, stringFormat);
            return stringRectangle;
        }

        private void DrawRectangle(Rectangle rectangle)
        {
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
        }

        public void SaveImage(string filename)
        {
            bitmap.Save(filename);
        }
    }
}