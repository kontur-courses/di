using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public interface IPaintConfig
    {
        List<Brush> WordsColors { get;}
        string FontName { get; }
        int FontSize { get; }
        Size ImageSize { get; }

        Brush GetRandomBrush();
    }
}
