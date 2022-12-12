using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers;

public class CloudDrawer : ICloudDrawer
{
    public Bitmap DrawWords(List<IDrawingWord> words, IDrawingSettings settings)
    {
        var minExpectedSize = FindSizeByRectangles(words);
        if (minExpectedSize.Height > settings.PictureSize.Height
            || minExpectedSize.Width > settings.PictureSize.Width)
            throw new Exception("User sizes less then min required sizes " +
                                       $"{minExpectedSize} > {settings.PictureSize}");
        return Draw(words, settings.PictureSize.Width, settings.PictureSize.Height, settings.BgColor);
    }
        
    private static Size FindSizeByRectangles(IReadOnlyCollection<IDrawingWord> words)
    {
        var width = words.Max(word => word.Rectangle.Right);
        var height = words.Max(word => word.Rectangle.Bottom);
        return new Size(width, height);
    }
        
    private static Bitmap Draw(
        IEnumerable<IDrawingWord> drawingWords,
        int width, int height,
        Color bgColor)
    {
        var myBitmap = new Bitmap(width, height);
        var graphics = Graphics.FromImage(myBitmap);
        graphics.Clear(bgColor);

        foreach (var word in drawingWords)
        {
            if (word == null)
                throw new NullReferenceException("Word can not be null");
            graphics.DrawString(
                word.Value,
                word.Font,
                new SolidBrush(word.Color),
                word.Rectangle.Location);
        }
        
        return myBitmap;
    }
}