using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizator;

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
            var sourceToDraw = objects
                .Select(o => TranslatePositionByHalfSize(o, Size));
            using (var bitmap = new Bitmap(Size.Width, Size.Height))
            {
                DrawElements(bitmap, sourceToDraw, Size);
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