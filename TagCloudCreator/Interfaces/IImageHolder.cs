using System.Drawing;
using TagCloudCreator.Domain.Settings;

namespace TagCloudCreator.Interfaces;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage();
}