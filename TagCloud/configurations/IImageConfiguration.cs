using System.Drawing;

namespace TagCloud.configurations
{
    public interface IImageConfiguration
    {
        Color GetBackgroundColor();
        int GetWidth();
        int GetHeight();
    }
}