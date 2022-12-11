using TagCloudGraphicalUserInterface.Settings;

namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface IImageSettingsProvider
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}
