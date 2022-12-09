using System.Drawing;
using TagCloud.ColoringAlgorithm;

namespace TagCloud.ImageGenerator;

public class BitmapImageGenerator : IImageGenerator
{
    private readonly Size size;
    private readonly IColoringAlgorithm coloringAlgorithm;

    public BitmapImageGenerator(Size size, IColoringAlgorithm coloringAlgorithm)
    {
        this.size = size;
        this.coloringAlgorithm = coloringAlgorithm;
    }
    
    public Image GenerateImage(Rectangle[] rectangles)
    {
        var image = new Bitmap(size.Width, size.Height);

        using var graphics = Graphics.FromImage(image);
        using var backgroundBrush = new SolidBrush(Color.White);
        
        graphics.FillRectangle(backgroundBrush,0, 0, image.Width, image.Height);
        foreach (var rectangle in rectangles)
        {
            using var rectanglePen = new Pen(coloringAlgorithm.GetNextColor());
            graphics.DrawRectangle(rectanglePen, rectangle);
        }

        return image;
    }
}