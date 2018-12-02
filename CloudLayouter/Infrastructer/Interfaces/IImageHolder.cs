using System.Drawing;
using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Infrastructer
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