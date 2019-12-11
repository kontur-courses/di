using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace TagsCloudContainer.Visualizer
{
    class DefaultVisualizerSettings : IVisualizerSettings
    {
        public Brush BackgroundBrush { get; } = Brushes.White;

        public Font GetFont(WordRectangle wordRectangle)
        {
            var size = wordRectangle.Rectangle.Height;
            return new Font(FontFamily.GenericMonospace, size, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public Brush GetBrush(WordRectangle wordRectangle)
        {
            return Brushes.Black;
        }
    }
}
