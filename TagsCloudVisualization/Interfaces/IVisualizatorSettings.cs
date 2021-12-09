using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface IVisualizatorSettings
    {
        public string Filename { get; }
        public Size BitmapSize { get; }
        public FontFamily FontFamily { get; }
        public Color BackgroundColor { get; }
        public float MinMargin { get; }
        public bool FillTags { get; }
    }
}