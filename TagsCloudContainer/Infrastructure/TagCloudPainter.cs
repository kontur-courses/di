using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TagCloudPainter
{
    private const int AddedImageSize = 300;
    private ICloudLayouter layouter;
    private Settings settings;

    public TagCloudPainter(ICloudLayouter layouter,
        Settings settings)
    {
        this.layouter = layouter;
        this.settings = settings;
    }

    public string Paint(IEnumerable<PaintedTag> tags)
    {
        var cloudTags = PutCloudTags(tags).ToList();
        var neededSize = CalculateCoverageSize(cloudTags.Select(tag => tag.Rectangle)) 
            + new Size(AddedImageSize, AddedImageSize);
        var imageOffset = neededSize / 2 - new Size(settings.Center);
        var scaleX = settings.ImageSize.Width / (float)neededSize.Width;
        var scaleY = settings.ImageSize.Height / (float)neededSize.Height;
        return DrawTags(scaleX, scaleY, cloudTags, imageOffset);
    }

    private string DrawTags(float scaleX, float scaleY, 
        IEnumerable<CloudTag> cloudTags, Size imageOffset)
    {
        var bm = new Bitmap(settings.ImageSize.Height, settings.ImageSize.Width);
        var graphics = Graphics.FromImage(bm);
        graphics.ScaleTransform(scaleX, scaleY);
        graphics.Clear(settings.Palette.Background);

        foreach (var tag in cloudTags)
        {
            tag.Rectangle.Offset(new Point(imageOffset));
            graphics.DrawString(tag.Text, tag.Label.Font,
                new SolidBrush(tag.Color), tag.Rectangle);
        }

        var layoutsPath = Path.Combine(Path.GetFullPath(@"..\..\..\"), "layouts");
        var savePath = $"{layoutsPath}\\layout_{DateTime.Now:HHmmssddMM}.{settings.Format}";
        bm.Save(savePath, settings.Format);
        return savePath;
    }

    private IEnumerable<CloudTag> PutCloudTags(IEnumerable<PaintedTag> tags)
    {
        var averageFrequency = tags.Select(tag => tag.Frequency).Sum()
            / tags.Count();

        foreach (var tag in tags)
        {
            var fontSize = (float)(tag.Frequency / averageFrequency) * settings.Font.Size;
            var label = new Label { AutoSize = true };
            label.Font = new Font(settings.Font.FontFamily, fontSize, settings.Font.Style);
            label.Text = tag.Text;
            var size = label.GetPreferredSize(settings.ImageSize);
            var rectangle = layouter.PutNextRectangle(size);
            yield return new CloudTag(tag, label, rectangle);
        }
    }

    private static Size CalculateCoverageSize(IEnumerable<Rectangle> rectangles)
    {
        var maxX = rectangles.Max(x => x.X + x.Size.Width);
        var minX = rectangles.Min(x => x.X);
        var width = maxX - minX;

        var maxY = rectangles.Max(x => x.Y);
        var minY = rectangles.Min(x => x.Y - x.Size.Height);
        var height = maxY - minY;

        return new Size(width, height);
    }
}