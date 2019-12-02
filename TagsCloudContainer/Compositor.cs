using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace TagsCloudContainer
{
    public class Compositor
    {
        private Setting setting;
        private IWordsTransformer transformer;
        private ICloudLayouter layouter;

        public Compositor(IWordsTransformer transformer, ICloudLayouter layouter, Setting setting)
        {
            this.transformer = transformer;
            this.setting = setting;
            this.layouter = layouter;
        }

        public void Composite()
        {
            var words = new HashSet<(Rectangle, string)>();
            foreach (var (sizeOfWord, word) in transformer.Transform())
            {
                var rectangle = layouter.PutNextRectangle(sizeOfWord);
                words.Add((rectangle, word));
            }

            var leftCornerPoint = new Point();
            var rightCornerPoint = new Point();
            foreach (var (rectangle, word) in words)
            {
                if (rectangle.Top < leftCornerPoint.Y)
                    leftCornerPoint.Y = rectangle.Top;

                if (rectangle.Left < leftCornerPoint.X)
                    leftCornerPoint.X = rectangle.Left;

                if (rectangle.Bottom > rightCornerPoint.Y)
                    rightCornerPoint.Y = rectangle.Bottom;

                if (rectangle.Right > rightCornerPoint.X)
                    rightCornerPoint.X = rectangle.Right;
            }

            using (var bitmap = new Bitmap(rightCornerPoint.X - leftCornerPoint.X,
                rightCornerPoint.Y - leftCornerPoint.Y))
            {
                var graphic = Graphics.FromImage(bitmap);
                foreach (var (rectangle, word) in words)
                {
                    var rectangleWithStep = new Rectangle(rectangle.Location - (Size) leftCornerPoint, rectangle.Size);
                    graphic.DrawString(word, setting.Font, setting.Brush, rectangleWithStep);                 
                }

                bitmap.Save("WordsCloud.png",System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}