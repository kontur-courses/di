using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Entities;


namespace TagsCloud.TagsCloudPainters;

public class SimplePainter
{
    private int margin = 10;
    public void DrawCloud(string filename, IEnumerable<Tag> tags)
    {
      
    }


    private void SaveImageToFile(Bitmap bitmap, string filename)
    {
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        var filePath = Path.Combine(projectDirectory, "images", filename);
        bitmap.Save(filePath, ImageFormat.Png);
    }

    private void SetImageSize(IEnumerable<Tag> tags, out int imageWidth, out int imageHeight)
    {
        var maxX = tags.Select(rec => rec.TagRectangle.X + rec.TagRectangle.Width / 2).Max();
        var minX = tags.Select(rec => rec.TagRectangle.X - rec.TagRectangle.Width / 2).Min();
        var maxY = tags.Select(rec => rec.TagRectangle.Y + rec.TagRectangle.Height / 2).Max();
        var minY = tags.Select(rec => rec.TagRectangle.Y - rec.TagRectangle.Height / 2).Min();

        imageWidth = (int)Math.Round(maxX - minX + margin);
        imageHeight = (int)Math.Round(maxY - minY + margin);
    }

    private IEnumerable<Tag> GetNewRectanglesPositions(IEnumerable<Tag> tags, int imageWidth,
        int imageHeight)
    {
        return tags.Select(tag =>
        {
            tag.TagRectangle.X = tag.TagRectangle.X + imageWidth / 2;
            tag.TagRectangle.Y = tag.TagRectangle.Y + imageHeight / 2;
            return tag;
        });
    }
}