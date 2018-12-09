using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;
using Point = TagCloud.Layouter.Point;

namespace TagCloud.Visualizer
{
    public class Visualizer : IVisualizer
    {
        private readonly IColorScheme colorScheme;
        private readonly IFontScheme fontScheme;

        public Visualizer(
            IColorScheme colorScheme,
            IFontScheme fontScheme,
            Color backgroundColor,
            Size imageSize)
        {
            this.colorScheme = colorScheme;
            this.fontScheme = fontScheme;
            BackgroundColor = backgroundColor;
            Size = imageSize;
        }

        public Color BackgroundColor { get; set; }
        public Size Size { get; }

        public Image Visualize(IEnumerable<PositionedElement> objects)
        {
            var source = PrepareToVisualize(objects);
            var sourceToDraw = source.Select(NormalizePosition);
            var bitmap = new Bitmap(Size.Width, Size.Height);
            DrawElements(bitmap, sourceToDraw, Size);
            return bitmap;
        }

        private IEnumerable<VisualElement> PrepareToVisualize(IEnumerable<PositionedElement> elements)
        {
            var result = new List<VisualElement>();
            foreach (var element in elements)
            {
                var color = colorScheme.Process(element);
                var font = fontScheme.Process(element);
                var newElement = new VisualElement(element, font, color);
                result.Add(newElement);
            }

            return result;
        }

        private Font AdjustFontSize(Graphics graphics, Font font, Size necessarySize, string word)
        {
            const float sizeIncrementInterval = 0.3f;
            var currentFont = new Font(font.FontFamily, 0.1f);
            var currentSize = graphics.MeasureString(word, currentFont).ToSize();
            while (currentSize.Width < necessarySize.Width
                   && currentSize.Height < necessarySize.Height)
            {
                currentFont = new Font(currentFont.FontFamily, currentFont.SizeInPoints + sizeIncrementInterval);
                currentSize = graphics.MeasureString(word, currentFont).ToSize();
            }

            return new Font(currentFont.FontFamily, currentFont.SizeInPoints - sizeIncrementInterval);
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
                var font = AdjustFontSize(graphics, element.Font, element.Size.ToSize(), element.Word);
                graphics.DrawString(element.Word, font, pen.Brush, ExtractRectangleF(element));
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