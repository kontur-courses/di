using System.Drawing;
using TagCloud.configurations;

namespace TagCloud.visual
{
    public interface IVisualizer
    {
        void FillImage(Image image, IImageConfiguration imageConfiguration);
    }
}