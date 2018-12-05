using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizator;

namespace TagsCloudVisualization.Visualizer
{
    public class VisualizerToFile : IVisualizer
    {
        public VisualizerToFile(string outputPath, Color backgroundColor)
        {
            Path = outputPath;
            BackgroundColor = backgroundColor;
        }

        public string Path { get; set; }
        public Color BackgroundColor { get; set; }

        public void Visualize(IEnumerable<VisualElement> objects, Size size)
        {
            var sourceToDraw = objects
                .Select(o => TranslatePositionByHalfSize(o, size));
            using (var bitmap = new Bitmap(size.Width, size.Height))
            {
                DrawElements(bitmap, sourceToDraw, size);
                bitmap.Save(Path);
            }
        }

        private void DrawElements(
            Bitmap bitmap,
            IEnumerable<VisualElement> elements,
            Size size)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TranslateTransform(size.Width, size.Height);
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

        private VisualElement TranslatePositionByHalfSize(VisualElement element, Size size)
        {
            var offset = new Point(size.Width / 2, size.Height / 2);
            var newPosition = element.Position + offset;
            return new VisualElement(
                element.Word, 
                newPosition, 
                element.Size, 
                element.Color, 
                element.Font, 
                element.Frequency);
        }
    }
}