using System.Drawing;
using System.Dynamic;
using TagCloudTests;

namespace TagCloud;

public class TagCloudDrawer : ICloudDrawer
{
    private readonly string path;
    private readonly IColorSelector selector;
    private readonly string name;
    private readonly int fontSize;

    private TagCloudDrawer(string path, string name, IColorSelector selector, int fontSize)
    {
        this.path = path;
        this.selector = selector;
        this.fontSize = fontSize;
        this.name = name;
    }

    public int FontSize => fontSize;

    public void Draw(IEnumerable<TextRectangle> rectangles)
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
                .Select(rect => new Rectangle(rect.X, rect.Y, rect.Width, rect.Height))
                .ToArray()
        );
        graphics.DrawStrings(
            selector, 
            rectangles
                .Select(rect => rect with { X = -minX + rect.X, Y = -minY + rect.Y })
                .ToArray()
        );
        
        SaveToFile(bitmap);
    }

    public Size GetTextRectangleSize(string text, int size)
    {
        var graphics = Graphics.FromImage(new Bitmap(1,1));
        var sizeF = graphics.MeasureString(text, new Font(FontFamily.GenericSerif, size));
        return new Size((int)sizeF.Width, (int)sizeF.Height);
    }

    private void SaveToFile(Bitmap bitmap)
    {
        var pathToFile = @$"{path}\{name}.jpg";
        bitmap.Save(pathToFile);
        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }

    public static TagCloudDrawer Create(string path, string name, int size, IColorSelector selector)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException("Directory does not exist");
        return new TagCloudDrawer(path, name, selector, size);
    }
}