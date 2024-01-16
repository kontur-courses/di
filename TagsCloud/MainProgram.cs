using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud;

public class MainProgram
{
    public static void Main(string[] args)
    {
        var spiral = new Spiral(new Point(100, 100));
        var layout = new CircularCloudLayouter(spiral);
         
        for (var i = 0; i < 10000; i++)
        {
            var rectangle = layout.PutNextRectangle(Utils.GetRandomSize());
        }
        
        var workingDirectory = Environment.CurrentDirectory;
        var imagesDirectoryPath = Path.Combine(workingDirectory, "images");
        
        if (!Directory.Exists(imagesDirectoryPath))
        {
            Directory.CreateDirectory(imagesDirectoryPath);
        }

        const string imageName = "1077rect";
        var imagePath = Path.Combine(imagesDirectoryPath, $"{imageName}.png");

        using var image = RectanglesVisualizer.GetTagsCloudImage(layout.Rectangles);
        image.Save(imagePath, ImageFormat.Png);
    }
}