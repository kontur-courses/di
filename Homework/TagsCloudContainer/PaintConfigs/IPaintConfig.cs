using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.PaintConfigs
{
    public interface IPaintConfig
    {
        ImageFormat ImageFormat { get; }
        IColorScheme Color { get;}
        string FontName { get; }
        int FontSize { get; }
        Size ImageSize { get; }
    }
}
