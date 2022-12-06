using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage();
}