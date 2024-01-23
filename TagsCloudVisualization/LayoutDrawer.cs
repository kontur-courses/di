using System.Drawing.Imaging;
using System.Drawing;

namespace TagsCloudVisualization;

public static class LayoutDrawer
{
    public static void CreateLayoutImage(IEnumerable<TextRectangle> createdTextRectangles, string fileName, string? filePath = null)
    {
        var (imageWidth, imageHeight) = (1000, 1000);

        using var bitmap = new Bitmap(imageWidth, imageHeight);
        using var graphics = Graphics.FromImage(bitmap);
        using var blackPen = new Pen(Color.Black);

        graphics.Clear(Color.Wheat);
        
        using var brush = new SolidBrush(Color.Black);
        foreach (var textRectangle in createdTextRectangles)
        {
            var x = textRectangle.Rectangle.X + imageWidth / 2;
            var y = textRectangle.Rectangle.Y + imageHeight / 2;
            graphics.DrawString(textRectangle.Text, textRectangle.Font, brush, x, y);
        }

        filePath ??= AppDomain.CurrentDomain.BaseDirectory + @"\Images";

        Directory.CreateDirectory(filePath);
        
        bitmap.Save(filePath + @$"\{fileName}.png", ImageFormat.Png);

        Console.WriteLine($"Image is saved to {filePath}" + @$"\{fileName}.png");
    }
}

