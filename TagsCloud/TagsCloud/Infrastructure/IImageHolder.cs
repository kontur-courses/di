using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public interface IImageHolder
    {
        ImageSettings Settings { get; set; }
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateCanvas(ImageSettings settings);
        void SaveImage(string fileName);
        void RenderWords(Dictionary<string, int> frequencyDictionary);
        void RedrawCurrentImage();
    }
}