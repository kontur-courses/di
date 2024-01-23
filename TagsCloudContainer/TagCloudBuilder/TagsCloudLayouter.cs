using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization
{
    public class TagsCloudLayouter
    {
        private readonly Point center;
        private readonly IPointsProvider pointsProvider;
        private readonly CloudDrawingSettings drawingSettings;
        private readonly IEnumerable<(string, int)> words;

        private readonly Graphics graphics;
        private readonly Image image;
        public ICollection<Rectangle> Cloud { get; private set; }
        public TagsCloudLayouter(Point center, IPointsProvider pointsProvider, CloudDrawingSettings drawingSettings, IEnumerable<(string, int)> words)
        {
            if (center.X <= 0 || center.Y <= 0)
                throw new ArgumentException("Central point coordinates should be in positive");

            this.center = center;
            this.pointsProvider = pointsProvider;
            this.drawingSettings = drawingSettings;
            this.words = words;

            image = new Bitmap(center.X * 2, center.Y * 2);
            graphics = Graphics.FromImage(image);
        }

        private IEnumerable<TextImage> GetTextImages(IEnumerable<(string, int)> words)
        {
            foreach (var word in words)
            {
                var font = new Font(drawingSettings.FontFamily, drawingSettings.FontSize + word.Item2);
                var size = (graphics.MeasureString(word.Item1, font) + new SizeF(1, 0)).ToSize(); ;
                var textImage = new TextImage(word.Item1, font, size);

                yield return textImage;
            }
        }

        private Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size width and height should be positive");

            if (Cloud == null || !Cloud.Any())
                return new Rectangle(center, rectangleSize);

            Rectangle rectangle;
            bool placingIsCorrect;

            var enumerator = pointsProvider.Points().GetEnumerator();
            enumerator.MoveNext();

            do
            {
                var point = enumerator.Current;
                enumerator.MoveNext();

                rectangle = new Rectangle(new Point(point.X - rectangleSize.Width / 2,
                        point.Y - rectangleSize.Height / 2),
                    rectangleSize);
                placingIsCorrect = IsPlacedCorrectly(rectangle, Cloud, new Size(center.X * 2, center.Y * 2));

            } while (!placingIsCorrect);

            return rectangle;
        }

        public Image ToImage()
        {
            var brush = new SolidBrush(Color.White);

            graphics.Clear(Color.Black);

            Cloud = new List<Rectangle>();

            foreach (var textImage in GetTextImages(words))
            {
                var rect = PutNextRectangle(textImage.Size);
                Cloud.Add(rect);

                graphics.DrawString(textImage.Text, textImage.Font, brush, rect);
            }

            return image;
        }

        private bool IsPlacedCorrectly(Rectangle rectangle, ICollection<Rectangle> rectanglesCloud, Size canvasSize)
        {
            if (rectangle.Top < 0 || rectangle.Left < 0 || rectangle.Bottom > canvasSize.Height ||
                rectangle.Right > canvasSize.Width)
                return false;

            foreach (var previous in Cloud)
            {
                if (rectangle.IntersectsWith(previous))
                    return false;
            }

            return true;
        }
    }
}
