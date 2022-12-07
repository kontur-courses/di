using System.Drawing.Imaging;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Domain;

public class PictureBoxImageHolder : PictureBox, IImageHolder
{
    private readonly IImageSaverProvider _imageSaverProvider;
    private readonly IImageSettingsProvider _imageSettingsProvider;

    public PictureBoxImageHolder(IImageSaverProvider imageSaverProvider, IImageSettingsProvider imageSettingsProvider)
    {
        _imageSaverProvider = imageSaverProvider;
        _imageSettingsProvider = imageSettingsProvider;
    }

    public Size GetImageSize()
    {
        FailIfNotInitialized();
        return Image.Size;
    }

    public Graphics StartDrawing()
    {
        FailIfNotInitialized();
        RecreateImage();
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

    public void RecreateImage()
    {
        var imageSettings = _imageSettingsProvider.GetImageSettings();
        Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
    }

    public void SaveImage()
    {
        FailIfNotInitialized();
        _imageSaverProvider.GetSaver().SaveImage(Image);
    }
}