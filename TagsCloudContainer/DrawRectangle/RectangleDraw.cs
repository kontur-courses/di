using System.Drawing;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.DrawRectangle.Interfaces;
using TagsCloudContainer.WordProcessing;
using Color = System.Drawing.Color;

namespace TagsCloudContainer.DrawRectangle;

public class RectangleDraw : IDraw
{
    private readonly Settings _settings;
    private readonly CircularCloudLayouter _layouter;
    
    public RectangleDraw(CircularCloudLayouter layouter, Settings settings)
    {
        _layouter = layouter;
        _settings = settings;
    }

    private Bitmap CreateBitmap(List<Rectangle> rectangles)
    {
        var width = rectangles.Max(rectangle => rectangle.Right) -
                    rectangles.Min(rectangle => rectangle.Left);
        var height = rectangles.Max(rectangle => rectangle.Bottom) -
                    rectangles.Min(rectangle => rectangle.Top);
        return new Bitmap(width * 2, height * 2);
    }
    public Bitmap CreateImage(List<Word> words)
    {
        var rectangles = WordRectangleGenerator.GenerateRectangles(words, _layouter, _settings);
        var bitmap = CreateBitmap(rectangles);
        var shiftToBitmapCenter = new Size(bitmap.Width / 2, bitmap.Height / 2);
        using var g = Graphics.FromImage(bitmap);
        using var pen = new Pen(_settings.Color);
        g.Clear(Color.Black);
        var count = 0;
        foreach (var word in words)
        {
            var rectangle = new Rectangle(
                rectangles[count].Location + shiftToBitmapCenter, 
                rectangles[count].Size);
            using var font = new Font(_settings.FontName, word.Size);
            g.DrawString(word.Value, font, pen.Brush, rectangle);
            count++;
        }

        return bitmap;
    }
}