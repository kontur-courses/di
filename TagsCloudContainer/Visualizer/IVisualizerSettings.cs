using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        Brush BackgroundBrush { get; }

        Font GetFont(WordRectangle wordRectangle);
        Brush GetBrush(WordRectangle wordRectangle);
    }
}
