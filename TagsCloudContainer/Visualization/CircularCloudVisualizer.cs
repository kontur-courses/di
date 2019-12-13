using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualization.Interfaces;

namespace TagsCloudContainer.Visualization
{
    public class CircularCloudVisualizer : IVisualizer
    {
        private readonly Pen rectangleBorderPen;
        private readonly Brush rectangleFillBrush;
        private readonly Brush backgroundFillBrush;
        private readonly Brush textBrush;
        private readonly ISaver saver;
        private readonly Size resolution;
        private readonly string fontName;

        public CircularCloudVisualizer(
            ColoringOptions options, ISaver saver, Size resolution, string fontName)
        {
            rectangleFillBrush = options.rectangleFillBrush;
            backgroundFillBrush = options.backgroundFillBrush;
            rectangleBorderPen = options.rectangleBorderPen;
            textBrush = options.textBrush;
            this.saver = saver;
            this.resolution = resolution;
            this.fontName = fontName;
        }

        public void Visualize(IEnumerable<WordRectangle> wordRectangles, string path)
        {
            var visualization = GetVisualization(wordRectangles);
            saver.SaveImage(path, visualization, resolution);
        }

        public Bitmap GetVisualization(IEnumerable<WordRectangle> rectangles)
        {
            var imageSize = GetImageSize(rectangles);
            if (imageSize.Width == 0 && imageSize.Height == 0)
            {
                return null;
            }

            return GetImage(imageSize, rectangles);
        }

        private static Size GetImageSize(IEnumerable<WordRectangle> rectangles)
        {
            var cloudRightBorder = rectangles.Max(rect => rect.Rectangle.Right);
            var cloudBottomBorder = rectangles.Max(rect => rect.Rectangle.Bottom);
            var cloudLeftBorder = rectangles.Min(rect => rect.Rectangle.Left);
            var cloudTopBorder = rectangles.Min(rect => rect.Rectangle.Top);
            return new Size((int) Math.Ceiling(cloudRightBorder + cloudLeftBorder),
                (int) Math.Ceiling(cloudBottomBorder + cloudTopBorder));
        }

        private Bitmap GetImage(Size imageSize, IEnumerable<WordRectangle> wordRectangles)
        {
            var image = new Bitmap(imageSize.Width, imageSize.Height);

            using (var graphics = Graphics.FromImage(image))
            {
                FillBackground(graphics, imageSize, backgroundFillBrush);

                foreach (var wordRectangle in wordRectangles)
                {
                    var rectangle = Rectangle.Round(wordRectangle.Rectangle);
                    graphics.FillRectangle(rectangleFillBrush, rectangle);
                    graphics.DrawRectangle(rectangleBorderPen, rectangle);
                    graphics.DrawString(
                        wordRectangle.SizedWord.Word,
                        new Font(fontName, wordRectangle.SizedWord.FontSize),
                        textBrush,
                        rectangle);
                }
            }

            return image;
        }

        private static void FillBackground(Graphics graphics, Size imageSize, Brush backgroundBrush)
        {
            var backgroundRectangle = new Rectangle(0, 0, imageSize.Width, imageSize.Height);
            graphics.FillRectangle(backgroundBrush, backgroundRectangle);
        }
    }
}