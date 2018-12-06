using System.Drawing;
using CloudLayouter.Infrastructer.Common.Settings;

namespace CloudLayouter.Infrastructer.Interfaces
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