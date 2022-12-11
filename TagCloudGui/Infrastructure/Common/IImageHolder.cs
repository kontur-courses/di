using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.TagCloudVisualizations;

namespace TagCloudGui.Infrastructure.Common
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ITagCloudVisualizationSettings settings);
        void SaveImage(string fileName, ImageFormat format);
    }
}