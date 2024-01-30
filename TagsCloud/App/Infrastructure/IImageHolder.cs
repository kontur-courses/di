using System.Drawing;
using TagsCloud.App.Settings;

namespace TagsCloud.Infrastructure;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage(string fileName);
}