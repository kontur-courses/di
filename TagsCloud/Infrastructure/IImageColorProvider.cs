using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public interface IImageColorProvider
    {
        Color GetColor();
        void AddColor(Color color);
    }
}