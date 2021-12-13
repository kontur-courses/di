using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public interface IPaintConfig
    {
        Brush Color { get;}
        string FontName { get; }
        int FontSize { get; }
        Size ImageSize { get; }
    }
}
