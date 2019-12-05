using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        Size ImageSize { get; }
        Color BackgroundColor { get; }
        Color TextColor { get; }
        FontFamily FontFamily { get; }
        FontStyle FontStyle { get; }
    }
}
