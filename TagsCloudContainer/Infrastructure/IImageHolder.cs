using System.Drawing;
using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure
{
    public interface IImageHolder
    {
        ImageSettings GetImageSettings();
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}