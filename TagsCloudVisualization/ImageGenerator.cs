using System.Drawing;

namespace TagsCloudVisualization;

public class ImageGenerator
{
    private static readonly Color[] Colors =
    {
        Color.Red,
        Color.Orange,
        Color.Yellow,
        Color.Green,
        Color.LightBlue,
        Color.Blue,
        Color.Purple
    };

    public Bitmap Generate(ICollection<Rectangle> rectangles, int widthOffset = 200, int heightOffset = 200)
    {
        var canvasParameters = CalculateCanvasParameters(rectangles, widthOffset, heightOffset);
        var canvas = new Bitmap(canvasParameters.Width, canvasParameters.Height);
        using var graphics = Graphics.FromImage(canvas);
        graphics.TranslateTransform(canvasParameters.Offset.X, canvasParameters.Offset.Y);

        var index = 0;
        foreach (var rectangle in rectangles)
        {
            using var pen = new Pen(Colors[index++ % Colors.Length]);
            graphics.DrawRectangle(pen, rectangle);
        }

        return canvas;
    }

    private CanvasParameters CalculateCanvasParameters(ICollection<Rectangle> rectangles, int widthOffset,
        int heightOffset)
    {
        var maxX = rectangles.Max(r => r.Right);
        var maxY = rectangles.Max(r => r.Bottom);
        var minX = rectangles.Min(r => r.Left);
        var minY = rectangles.Min(r => r.Top);
        var width = maxX - minX;
        var height = maxY - minY;

        return new CanvasParameters
        {
            Width = width + widthOffset,
            Height = height + heightOffset,
            Offset = new Point((width + widthOffset) / 2, (height + heightOffset) / 2)
        };
    }
}