using System.Drawing;
#pragma warning disable CA1416

namespace TagsCloudVisualization;

public class LayoutDrawer
{
    private readonly Pen _pen;

    public LayoutDrawer(Pen pen)
    {
        _pen = pen;
    }

    public void Draw(Dictionary<string, Rectangle> wordsToSizes, string filename)
    {
        var rectangles = wordsToSizes.Values.ToList();
        var size = GetSize(rectangles);
        var bitmap = new Bitmap(size.Width, size.Height);
        var graphics = Graphics.FromImage(bitmap);
        foreach (var key in wordsToSizes.Keys)
        {
            var curRectangle = wordsToSizes[key];
            var centredRectangle = CentreRectangle(curRectangle, rectangles[0].Location, bitmap);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            
            var primarySize = graphics.MeasureString(key, new Font("Times", 1));
            var w = centredRectangle.Width / primarySize.Width;
            var h = centredRectangle.Height / primarySize.Height;

            var font = w<= h? new Font("Times", w) : new Font("Times", h);


            graphics.DrawString(key, font, Brushes.White, centredRectangle, sf);
        }

        bitmap.Save(filename);
    }

    private Size GetSize(List<Rectangle> rectangles)
    {
        var leftBound = rectangles.Min(rectangle => rectangle.Left);
        var rightBound = rectangles.Max(rectangle => rectangle.Right);
        var bottomBound = rectangles.Max(rectangle => rectangle.Bottom);
        var topBound = rectangles.Min(rectangle => rectangle.Top);

        var width = rightBound - leftBound;
        var height = bottomBound - topBound;

        return new Size(width, height);
    }

    private Rectangle CentreRectangle(Rectangle rectangle, Point center, Bitmap bitmap)
    {
        var X = rectangle.Location.X - center.X + bitmap.Width / 2;
        var Y = rectangle.Location.Y - center.Y + bitmap.Height / 2;
        rectangle.Location = new Point(X, Y);
        return rectangle;
    }
}
#pragma warning restore CA1416