using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Common.Options;

namespace TagCloud.Common.Drawing;

public class CloudDrawer : ICloudDrawer
{
    public DrawingOptions CloudDrawingOptions { get; }

    public CloudDrawer(DrawingOptions cloudDrawingOptions)
    {
        CloudDrawingOptions = cloudDrawingOptions;
    }

    public Bitmap DrawCloud(IEnumerable<Tag> tags)
    {
        var newbmp = new Bitmap(CloudDrawingOptions.ImageSize.Width, CloudDrawingOptions.ImageSize.Height);
        using (var graphics = Graphics.FromImage(newbmp))
        {
            graphics.Clear(CloudDrawingOptions.BackgroundColor);
            graphics.TranslateTransform(newbmp.Width / 2, newbmp.Height / 2);
            foreach (var tag in tags)
            {
                var pen = new Pen(CloudDrawingOptions.TextColor);
                // раскомментить чтобы посмотреть границы слов ->
                //graphics.DrawRectangle(pen, tag.Bounds);
                graphics.DrawString(tag.Word, tag.Font, new SolidBrush(pen.Color), tag.Bounds);
            }
        }

        return newbmp;
    }
}