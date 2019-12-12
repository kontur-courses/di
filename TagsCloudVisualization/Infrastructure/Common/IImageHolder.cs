using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void ResizeImage(ImageSettings settings);
        void SaveImage(string fileName, ImageFormat format);
    }
}