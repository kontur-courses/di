using System.Drawing;
using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure
{
    public interface IImageHolder
    {
        AppSettings GetAppSettings();
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(AppSettings appSettings);
        void SaveImage(string fileName);
    }
}