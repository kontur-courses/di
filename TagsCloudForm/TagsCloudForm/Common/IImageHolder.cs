using System.Drawing;

namespace TagsCloudForm.Common
{
    public interface IImageHolder
    {
        Size GetImageSize();
        IGraphicDrawer StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}