using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication.Infrastructure;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage(string fileName);
}