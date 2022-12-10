using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.CloudDrawer;

public class DefaultCloudDrawer : ICloudDrawer
{
    private const string DefaultPath = "..\\..\\..\\Result.png";

    private Bitmap bitmap;
    private Graphics graphics;
    private Brush brush;

    public DefaultCloudDrawer(int windowWidth, int windowHeight, Brush brush)
    {
        bitmap = new Bitmap(windowWidth, windowHeight);
        graphics = Graphics.FromImage(bitmap);
        this.brush = brush;
    }

    public void Draw(List<TextLabel> wordsInPoint)
    {
        foreach (var textLabel in wordsInPoint)
        {
            graphics.DrawString(textLabel.Content, textLabel.Font, brush, textLabel.Position);
        }

        SaveImage();
    }

    public void SaveImage()
    {
        bitmap.Save(DefaultPath, ImageFormat.Png);
    }
}