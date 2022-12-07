using System.Drawing;
using TagsCloudContainer.Options;

namespace TagsCloudContainer.Drawing;

public class CloudDrawer : ICloudDrawer
{
    public void DrawCloud(IEnumerable<Tag> tags, VisualizationOptions visualizationOptions)
    {
        var savePath = Directory.GetCurrentDirectory();
        var random = new Random();
        var newbmp = new Bitmap(visualizationOptions.ImageSize.Width, visualizationOptions.ImageSize.Height);
        var path = Path.Combine(savePath,
            random.Next(256) + "." + visualizationOptions.SavingImageFormat.ToString().ToLower());
        using (var graphics = Graphics.FromImage(newbmp))
        {
            graphics.Clear(Color.Azure);
            graphics.TranslateTransform(newbmp.Width / 2, newbmp.Height / 2);
            foreach (var tag in tags)
            {
                var pen = new Pen(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                graphics.DrawRectangle(pen, tag.Bounds);
                graphics.DrawString(tag.Word, new Font("Arial", tag.FontSize, FontStyle.Regular, GraphicsUnit.Pixel),
                    new SolidBrush(pen.Color), tag.Bounds);
            }
        }

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        newbmp.Save(path, visualizationOptions.SavingImageFormat);
        newbmp.Dispose();
    }
}