using System.Drawing;

namespace TagCloudCreator.Interfaces;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage();
    void SaveImage();
}