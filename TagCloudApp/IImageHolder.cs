using TagCloudApp.Domain;

namespace TagCloudApp;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage(string fileName);
}