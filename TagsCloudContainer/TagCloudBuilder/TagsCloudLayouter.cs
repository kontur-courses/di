using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.SettingsClasses;

namespace TagsCloudVisualization
{
    public class TagsCloudLayouter
    {
        private Point center;
        private IPointsProvider pointsProvider;
        private CloudDrawingSettings drawingSettings;
        private IEnumerable<(string, int)> words;
        private Graphics graphics;
        private Image image;

        private ICollection<Rectangle> Cloud { get; set; }

        public void Initialize(CloudDrawingSettings drawingSettings, IEnumerable<(string, int)> words)
        {
            if (drawingSettings.Size.Width <= 0 || drawingSettings.Size.Height <= 0)
                throw new ArgumentException("Size should be in positive");

            center = new Point(drawingSettings.Size.Width / 2, drawingSettings.Size.Height / 2);
            pointsProvider = drawingSettings.PointsProvider;
            pointsProvider.Initialize(center);
            this.drawingSettings = drawingSettings;
            this.words = words;

            image = new Bitmap(drawingSettings.Size.Width, drawingSettings.Size.Height);
            graphics = Graphics.FromImage(image);
        }

        public IEnumerable<TextImage> GetTextImages()
        {
            Cloud = new List<Rectangle>();

            var colors = ColorMapper.MapColors(words.Select(x => x.Item2), drawingSettings.Colors);

            foreach (var word in words)
            {
                var font = new Font(drawingSettings.FontFamily, drawingSettings.FontSize + word.Item2);
                var size = (graphics.MeasureString(word.Item1, font) + new SizeF(1, 0)).ToSize();

                var rect = PutNextRectangle(size);
                Cloud.Add(rect);

                var textImage = new TextImage(
                    word.Item1, font, size, colors[word.Item2], new Point(rect.X, rect.Y));

                yield return textImage;
            }
        }

        private bool IsPlacedCorrectly(Rectangle rectangle, ICollection<Rectangle> rectanglesCloud, Size canvasSize)
        {
            if (rectangle.Top < 0 || rectangle.Left < 0 || rectangle.Bottom > canvasSize.Height ||
                rectangle.Right > canvasSize.Width)
                return false;

            return !Cloud.Any(x => x.IntersectsWith(rectangle));
        }

        private Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size width and height should be positive");

            if (Cloud == null || !Cloud.Any())
                return new Rectangle(center, rectangleSize);

            Rectangle rectangle;
            bool placingIsCorrect;

            using var enumerator = pointsProvider.Points().GetEnumerator();
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
    }
}