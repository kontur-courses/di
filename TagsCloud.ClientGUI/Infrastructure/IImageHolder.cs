using System.Drawing;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}