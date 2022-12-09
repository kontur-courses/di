using System.Drawing;
using System.Runtime.InteropServices;
using TagCloud.ColoringAlgorithm;
using TagCloud.LayoutAlgorithm;

namespace TagCloud.ImageGenerator;

public class BitmapImageGenerator : IImageGenerator
{
    private readonly Size size;
    private readonly IColoringAlgorithm coloringAlgorithm;
    private readonly Font font;
    private readonly ILayoutAlgorithm layoutAlgorithm;

    public BitmapImageGenerator(Size size, IColoringAlgorithm coloringAlgorithm, Font font,
        ILayoutAlgorithm layoutAlgorithm)
    {
        this.size = size;
        this.coloringAlgorithm = coloringAlgorithm;
        this.font = font;
        this.layoutAlgorithm = layoutAlgorithm;
    }
    
    public Image GenerateImage(Tag[] tags)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            throw new NotSupportedException();
        
        var image = new Bitmap(size.Width, size.Height);

        using var graphics = Graphics.FromImage(image);
        using var backgroundBrush = new SolidBrush(Color.White);
        
        graphics.FillRectangle(backgroundBrush,0, 0, image.Width, image.Height);
        var colors = coloringAlgorithm.GetColors(tags.Length);
        var i = 0;
        foreach (var tag in tags)
        {
            var tagFont = new Font(font.FontFamily, tag.Size);
            var measuredTag = graphics.MeasureString(tag.Word, tagFont);
            var tagSize = new Size((int)Math.Ceiling(measuredTag.Width), (int)Math.Ceiling(measuredTag.Height));
            var position = layoutAlgorithm.PutNextRectangle(tagSize).Location;
            using var tagBrush = new SolidBrush(colors[i++]);
            graphics.DrawString(tag.Word, tagFont, tagBrush, position);
        }

        return image;
    }
}