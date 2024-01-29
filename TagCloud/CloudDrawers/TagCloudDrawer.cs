using System.Drawing;
using TagCloud.Extensions;
using TagCloudTests;

namespace TagCloud;

public class TagCloudDrawer : ICloudDrawer
{
    private readonly string path;
    private readonly IColorSelector selector;
    private readonly string name;
    private readonly Font font;

    private TagCloudDrawer(string path, string name, IColorSelector selector, Font font)
    {
        this.path = path;
        this.selector = selector;
        this.font = font;
        this.name = name;
    }

    public void Draw(List<TextRectangle> rectangles)
    {
        if (rectangles.Count == 0)
            throw new ArgumentException("Empty rectangles list");
        
        var containingRect = rectangles
            .Select(r => r.Rectangle)
            .GetMinimalContainingRectangle();

        using var bitmap = new Bitmap(containingRect.Width + 2, containingRect.Height + 2);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.DrawStrings(
            selector, 
            rectangles
                .Select(rect => rect.OnLocation(-containingRect.X + rect.X, -containingRect.Y + rect.Y))
                .ToArray()
        );
        
        SaveToFile(bitmap);
    }

    private void SaveToFile(Bitmap bitmap)
    {
        var pathToFile = @$"{path}\{name}";
        bitmap.Save(pathToFile);
        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }

    public static TagCloudDrawer Create(string path, string name, Font font, IColorSelector selector)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException("Directory does not exist");
        return new TagCloudDrawer(path, name, selector, font);
    }
}