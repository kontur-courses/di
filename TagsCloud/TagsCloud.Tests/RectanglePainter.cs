using System.Drawing;
using TagsCloud.Core.Helpers;

namespace TagsCloud.Tests;

public class RectanglePainter
{
    private static readonly Color defaultBackgroundColor = Color.Cornsilk;
    private static readonly Color defaultRectangleColor = Color.Chartreuse;
    private static readonly Color defaultRectangleBorderColor = Color.DarkRed;

    public static Image GetRandomColorTagCloudImage(List<Rectangle> rectangles, Size imageSize, Point shift)
    {
        var rnd = new Random();

        var colors = rectangles
            .Select(_ => Color.FromArgb(rnd.Next(100, 255), rnd.Next(100, 255), rnd.Next(100, 255)))
            .ToList();

        return GetTagCloudImage(rectangles, colors, imageSize, shift);
    }

    public static Image GetTagCloudImage(List<Rectangle> rectangles, Size imageSize, Point cloudPosition)
    {
        return GetTagCloudImage(rectangles, new List<Color>(), imageSize, cloudPosition);
    }

    public static Image GetTagCloudImage(List<Rectangle> rectangles, List<Color> colors, Size imageSize,
        Point cloudPosition)
    {
        var result = new Bitmap(imageSize.Width, imageSize.Height);

        var shiftedRectangles = rectangles
            .Select(r => new Rectangle(r.Location.Plus(cloudPosition), r.Size)).ToArray();

        var borderPen = new Pen(defaultRectangleBorderColor);
        var rectangleBrush = new SolidBrush(defaultRectangleColor);
        var background = new Rectangle(0, 0, imageSize.Width, imageSize.Height);

        using var graphics = Graphics.FromImage(result);
        graphics.FillRectangle(new SolidBrush(defaultBackgroundColor), background);

        for (var i = 0; i < rectangles.Count; i++)
        {
            var rectangle = shiftedRectangles[i];
            rectangleBrush.Color = i < colors.Count ? colors[i] : defaultRectangleColor;

            graphics.FillRectangle(rectangleBrush, rectangle);
            graphics.DrawRectangle(borderPen, rectangle);
        }

        return result;
    }
}