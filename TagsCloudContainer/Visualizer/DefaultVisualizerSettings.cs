using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace TagsCloudContainer.Visualizer
{
    class DefaultVisualizerSettings : IVisualizerSettings
    {
        public Size ImageSize { get; }
        public Color BackgroundColor { get; } = Color.White;
        public Color TextColor { get; } = Color.Black;
        public FontFamily FontFamily { get; } = FontFamily.GenericMonospace;
        public FontStyle FontStyle { get; } = FontStyle.Regular;

        public DefaultVisualizerSettings(Size imageSize)
        {
            ImageSize = imageSize;
        }
    }
}
