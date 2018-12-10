using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}