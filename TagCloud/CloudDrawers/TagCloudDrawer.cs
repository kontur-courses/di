using System.Drawing;
using System.Dynamic;
using TagCloudTests;

namespace TagCloud;

public class TagCloudDrawer : ICloudDrawer
{
    private readonly string path;
    private readonly IColorSelector selector;

    private TagCloudDrawer(string path, IColorSelector selector)
    {
        this.path = path;
        this.selector = selector;
    }

    public void Draw(IEnumerable<Rectangle> rectangles, string name)
    {
        if (rectangles.Count() == 0)
            throw new ArgumentException("Empty rectangles list");

        var minX = rectangles.Min(rect => rect.X);
        var maxX = rectangles.Max(rect => rect.Right);
        var minY = rectangles.Min(rect => rect.Top);
        var maxY = rectangles.Max(rect => rect.Bottom);

        using var bitmap = new Bitmap(maxX - minX + 2, maxY - minY + 2);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.DrawRectangles(
            selector, 
            rectangles
                .Select(rect => rect with { X = -minX + rect.X, Y = -minY + rect.Y })
                .ToArray()
        );
        SaveToFile(bitmap, name);
    }

    private void SaveToFile(Bitmap bitmap, string name)
    {
        var pathToFile = @$"{path}\{name}.jpg";
        bitmap.Save(pathToFile);
        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }

    public static TagCloudDrawer Create(string path, IColorSelector selector)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException("Directory does not exist");
        return new TagCloudDrawer(path, selector);
    }
}