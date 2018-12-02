using System.Drawing;

namespace FractalPainting.Infrastructure.Common
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