using System.Drawing;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Domain;

namespace TagCloudCreator.Interfaces;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage();
}