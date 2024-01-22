using System.Drawing.Imaging;
using System.Drawing;

namespace TagsCloudVisualization;

public static class LayoutDrawer
{
    public static void CreateLayoutImage(IEnumerable<Rectangle> createdRectangles, string fileName, string? filePath = null)
    {
        var (imageWidth, imageHeight) = DetermineImageWidthAndImageHeight(createdRectangles);

        using var bitmap = new Bitmap(imageWidth, imageHeight);
        using var graphics = Graphics.FromImage(bitmap);
        using var blackPen = new Pen(Color.Black);

        graphics.Clear(Color.Wheat);

        var offsettedRectangles = createdRectangles.ToArray();

        for (var i = 0; i < offsettedRectangles.Length; i++)
        {
            offsettedRectangles[i].Offset(imageWidth / 2, imageHeight / 2);
        }

        filePath ??= AppDomain.CurrentDomain.BaseDirectory + @"\Images";

        Directory.CreateDirectory(filePath);

        graphics.DrawRectangles(blackPen, offsettedRectangles);
        bitmap.Save(filePath + @$"\{fileName}.png", ImageFormat.Png);

        Console.WriteLine($"Image is saved to {filePath}" + @$"\{fileName}.png");
    }

    private static (int imageWidth, int imageHeight) DetermineImageWidthAndImageHeight(IEnumerable<Rectangle> createdRectangles)
    {
        var imageWidth = 0;
        var imageHeight = 0;

        foreach (var rectangle in createdRectangles)
        {
            imageWidth = new[] { imageWidth, Math.Abs(rectangle.Right), Math.Abs(rectangle.Left) }.Max();
            imageHeight = new[] { imageHeight, Math.Max(Math.Abs(rectangle.Top), Math.Abs(rectangle.Bottom)) }.Max();
        }

        imageWidth = 2 * imageWidth + 100;
        imageHeight = 2 * imageHeight + 100;

        return (imageWidth, imageHeight);
    }
}

