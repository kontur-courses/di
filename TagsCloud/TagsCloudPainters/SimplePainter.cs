using System.Drawing;
using System.Drawing.Imaging;
using Autofac.Features.AttributeFilters;
using CommandLine;
using TagsCloud.ColorGenerators;
using TagsCloud.ConsoleCommands;
using TagsCloud.Entities;
using TagsCloud.Layouters;


namespace TagsCloud.TagsCloudPainters;

public class SimplePainter:IPainter
{
    private readonly IColorGenerator colorGenerator;
    private readonly string filename;
    private readonly Size imageSize;
    private readonly Color backgroundColor;

    public SimplePainter(IColorGenerator colorGenerator,Options options)
    {
        this.colorGenerator = colorGenerator;
        this.filename = options.OutputFile;
        this.imageSize = options.ImageSize;
        this.backgroundColor = Color.FromName(options.Background);
    }

    public void DrawCloud(ILayouter layouter)
    {
        var tags = layouter.GetTagsCollection();
        if (!tags.Any())
            throw new ArgumentException();
        var bitmapsize = imageSize.IsEmpty ? layouter.GetImageSize() : imageSize;

        using (var bitmap = new Bitmap(bitmapsize.Width, bitmapsize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(backgroundColor);
                foreach (var tag in tags)
                {
                    var color = colorGenerator.GetTagColor(tag);
                    var brush = new SolidBrush(color);
                    g.DrawString(tag.Content,tag.Font,brush,GetTagPositionOnImage(tag.TagRectangle.Location,imageSize));
                }
            }
            SaveImageToFile(bitmap,filename);
        }
    }

    private Point GetTagPositionOnImage(Point position,Size size)
    {
        var x = position.X + size.Width/2;
        var y = position.Y + size.Height/2;

        return new Point(x, y);
    }
    private void SaveImageToFile(Bitmap bitmap, string filename)
    {
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        Console.WriteLine(projectDirectory);
        bitmap.Save(filename, ImageFormat.Png);
    }
    
}