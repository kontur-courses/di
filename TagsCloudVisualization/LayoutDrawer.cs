using System.Drawing.Imaging;
using System.Drawing;

namespace TagsCloudVisualization;

public static class LayoutDrawer
{
    public static void CreateLayoutImage(IEnumerable<TextRectangle> createdTextRectangles,
        Size imageSize,
        Palette palette,
        string fileName,
        string? filePath)
    {
        using var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(palette.BackgroundColor);

        using var brush = new SolidBrush(palette.TextColor);
        foreach (var textRectangle in createdTextRectangles)
        {
            var x = textRectangle.Rectangle.X + imageSize.Width / 2;
            var y = textRectangle.Rectangle.Y + imageSize.Height / 2;
            graphics.DrawString(textRectangle.Text, textRectangle.Font, brush, x, y);
        }

        filePath ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        Directory.CreateDirectory(filePath);

        bitmap.Save(Path.Combine(filePath, $"{fileName}.png"), ImageFormat.Png);

        Console.WriteLine($"Image is saved to {filePath}" + @$"\{fileName}.png");
    }
}