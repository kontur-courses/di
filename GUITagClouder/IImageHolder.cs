using System.Drawing;
using TagCloud;

namespace GUITagClouder
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(DrawingSettings newSettings);
        void RecreateImage(CloudSettings newSettings);
        void RecreateImage();
        void SaveImage();
    }
}