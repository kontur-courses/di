using System.Drawing.Imaging;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Domain;

public class PictureBoxImageHolder : PictureBox, IImageHolder
{
    private readonly IImageSaverProvider _imageSaverProvider;

    public PictureBoxImageHolder(IImageSaverProvider imageSaverProvider)
    {
        _imageSaverProvider = imageSaverProvider;
    }

    public Size GetImageSize()
    {
        FailIfNotInitialized();
        return Image.Size;
    }

    public Graphics StartDrawing()
    {
        FailIfNotInitialized();
        return Graphics.FromImage(Image);
    }

    private void FailIfNotInitialized()
    {
        if (Image == null)
            throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
    }

    public void UpdateUi()
    {
        Refresh();
        Application.DoEvents();
    }

    public void RecreateImage(ImageSettings imageSettings)
    {
        Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
    }

    public void SaveImage()
    {
        FailIfNotInitialized();
        _imageSaverProvider.GetSaver().SaveImage(Image);
    }
}