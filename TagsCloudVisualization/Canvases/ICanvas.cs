using System.Drawing;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.Canvases
{
    public interface ICanvas
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}