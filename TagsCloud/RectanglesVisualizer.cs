using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud;

public static class RectanglesVisualizer
{

    public static void RenderCloudImage(List<Tag> tags, Graphics graphics, IImageHolder imageHolder)
    {
        var sizeImage = imageHolder.GetImageSize();
        var background = new SolidBrush(Color.Black);
        graphics.FillRectangle(background, new Rectangle(0, 0, sizeImage.Width, sizeImage.Height));
        DrawTagsCloud(tags, graphics);
    }

    private static void DrawTagsCloud(List<Tag> tags, Graphics graphics)
    {
        foreach (var tag in tags)
        {
            var brush = new SolidBrush(tag.Color);
            graphics.DrawString(tag.Word, tag.Font, brush, tag.Rectangle.Location);
                
        }
    }
}