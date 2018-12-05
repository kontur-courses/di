using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizator;
using Point = TagsCloudVisualization.Layouter.Point;

namespace TagsCloudVisualization.Visualizer
{
    public class VisualizerToFile : IVisualizer
    {
        public VisualizerToFile(string outputPath, Color backgroundColor, Size imageSize)
        {
            Path = outputPath;
            BackgroundColor = backgroundColor;
            Size = imageSize;
        }

        public string Path { get; set; }
        public Color BackgroundColor { get; set; }
        public Size Size { get; }

        public void Visualize(IEnumerable<VisualElement> objects)
        {
            var sourceToDraw = objects.Select(NormalizePosition);
            using (var bitmap = new Bitmap(Size.Width, Size.Height))
            {
                DrawElements(bitmap, sourceToDraw, Size);
                bitmap.Save(Path);
            }
        }

        private VisualElement NormalizePosition(VisualElement element)
        {
            var newPosition = new Point(
                (int) (element.Position.X - element.Size.Width / 2),
                (int) (element.Position.Y - element.Size.Height / 2));
            element.Position = newPosition;
            return element;
        }

        private void DrawElements(
            Bitmap bitmap,
            IEnumerable<VisualElement> elements,
            Size size)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TranslateTransform(size.Width / 2, size.Height / 2);
                graphics.Clear(BackgroundColor);
                foreach (var element in elements)
                    DrawElement(graphics, element);
            }
        }

        private void DrawElement(Graphics graphics, VisualElement element)
        {
            using (var pen = new Pen(element.Color))
            {
                graphics.DrawString(element.Word, element.Font, pen.Brush, ExtractRectangleF(element));
            }
        }

        private RectangleF ExtractRectangleF(VisualElement element)
        {
            var locationF = element.Position.ToPointF();
            var sizeF = element.Size.ToSizeF();
            return new RectangleF(locationF, sizeF);
        }
    }
}