using System.Drawing;

namespace TagCloud.configurations
{
    public interface ITagConfiguration
    {
        Color GetColor();
        Font GetFont();
    }
}