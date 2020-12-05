using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouters;

namespace TagsCloud.Infrastructure
{
    public interface IImageHolder
    {
        ImageSettings Settings { get; set; }
        void ChangeLayouter(ICloudLayouter layouter);
        void RecreateCanvas(ImageSettings settings);
        void SaveImage(string fileName);
        void RenderWords(Dictionary<string, int> frequencyDictionary);
        void RedrawCurrentImage();
    }
}