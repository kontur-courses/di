using System.Drawing;

namespace TagsCloudVisualization.Common;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void RecreateImage(ImageSettings settings);
    void SaveImage(string fileName);
}