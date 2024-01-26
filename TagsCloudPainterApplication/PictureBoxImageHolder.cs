using System.Drawing.Imaging;
using TagsCloudPainterApplication.Infrastructure;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication;

public class PictureBoxImageHolder : PictureBox, IImageHolder
{
    private readonly Lazy<ImageSettings> imageSettings;

    public PictureBoxImageHolder(Lazy<ImageSettings> imageSettings)
    {
        this.imageSettings = imageSettings ?? throw new ArgumentNullException(nameof(imageSettings));
    }

    public Size GetImageSize()
    {
        return GetImage().Size;
    }

    public Graphics StartDrawing()
    {
        return Graphics.FromImage(GetImage());
    }

    public void UpdateUi()
    {
        Refresh();
        Application.DoEvents();
    }

    public void SaveImage(string fileName)
    {
        GetImage().Save(fileName);
    }

    public Image GetImage()
    {
        if (Image == null || Image.Width != imageSettings.Value.Width || Image.Height != imageSettings.Value.Height)
            Image = new Bitmap(imageSettings.Value.Width, imageSettings.Value.Height, PixelFormat.Format24bppRgb);

        return Image;
    }
}