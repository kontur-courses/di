using System.Drawing;

namespace GuiClient
{
    public interface IImageHolder
    {
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage();
        void SaveImage();
        void GenerateImage();
    }
}