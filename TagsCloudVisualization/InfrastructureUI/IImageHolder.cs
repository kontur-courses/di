using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI
{
    public interface IImageHolder
    {
        Size GetImageSize();

        Graphics StartDrawing();

        void UpdateUi();

        void RecreateImage(ImageSettings settings);

        void SaveImage(string fileName);
    }
}