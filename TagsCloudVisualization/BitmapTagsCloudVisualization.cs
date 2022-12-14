using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualization;

public class BitmapTagsCloudVisualization 
{
    public void SaveTagsCloud(CircularCloudLayouter layouter, string path)
    {
        if (!layouter.Rectangles.Any())
            return;

        var bitmap = DrawRectangles(layouter);
        bitmap.Save(path);
    }


    private static Bitmap DrawRectangles(CircularCloudLayouter layouter)
    {
        var maxWidthDiff = 200 + layouter.Rectangles.Max(rec => rec.Right) - layouter.Rectangles.Min(rec => rec.Left);
        var maxHeightDiff = 200 + layouter.Rectangles.Max(rec => rec.Top) - layouter.Rectangles.Min(rec => rec.Bottom);

        var squareWidth = Math.Max(maxWidthDiff, maxHeightDiff);

        var bitmap = new Bitmap((int)squareWidth, (int)squareWidth);

        var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(Color.White);
        var pen = new Pen(Brushes.Black);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        foreach (var rectangle in layouter.Rectangles)
        {
            var offsetRectangle = GetRectangleOffsetToCenter(rectangle, bitmap);
            graphics.DrawRectangle(pen, offsetRectangle);
        }

        return bitmap;
    }

    private static RectangleF GetRectangleOffsetToCenter(RectangleF rectangle, Image bitmap)
    {
        var widthShift = (float)bitmap.Width / 2;
        var heightShift = (float)bitmap.Height / 2;

        var rectanglePosition = new PointF(rectangle.Location.X + widthShift, rectangle.Location.Y + heightShift);
        return new RectangleF(rectanglePosition, rectangle.Size);
    }
}