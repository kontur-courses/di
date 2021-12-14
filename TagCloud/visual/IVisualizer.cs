using System.Drawing;
using TagCloud.configurations;

namespace TagCloud.visual
{
    public interface IVisualizer
    {
        Image GetImage(IImageConfiguration imageConfiguration);
    }
}