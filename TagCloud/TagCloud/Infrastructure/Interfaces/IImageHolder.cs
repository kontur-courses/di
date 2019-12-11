using System.Drawing;

namespace TagCloud
{
    public interface IImageHolder
    {
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}
