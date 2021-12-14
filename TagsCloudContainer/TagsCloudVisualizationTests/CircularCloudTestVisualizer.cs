using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualizationTests
{
    // Этот класс был временным для задачи TagCloud 
    // По итогу написал нормальный визуализатор для самой задачи, а этот решил оставить только для тестов
    // Так как он позволяет просто визуализировать именно прямоугольники
    #pragma warning disable CA1416
    public class CircularCloudTestVisualizer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly string path;
        private readonly Pen pen;

        public CircularCloudTestVisualizer(
            int imageWidth,
            int imageHeight,
            Color penColor,
            string imageSavingPath = "../../../TagsCloudVisualizations/")
        {
            path = imageSavingPath;
            Directory.CreateDirectory(imageSavingPath);
            
            bitmap = new Bitmap(imageWidth, imageHeight);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(penColor);
        }

        private void PutRectangle(RectangleF rectangle)
        {
            graphics.DrawRectangles(pen, new[] {rectangle});
        }

        public void PutRectangles(IEnumerable<RectangleF> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                PutRectangle(rectangle);
            }
        }

        public string SaveImage(string imageName="TagsCloudVisualization.png")
        {
            var savingPath = path + imageName;
            bitmap.Save(savingPath);
            return savingPath;
        }
    }
}