using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Visualizators
{
    public class TagsVisualizatorSettings : IVisualizatorSettings
    {
        public string Filename { get; }
        public Size BitmapSize { get; }
        public FontFamily FontFamily { get; }
        public Color BackgroundColor { get; }
        public float MinMargin { get; }
        public bool FillTags { get; }

        public TagsVisualizatorSettings(string filename) : 
            this(filename, new Size(800,800))
        {

        }

        public TagsVisualizatorSettings (string filename, Size bitmapSize) :
            this(filename, bitmapSize, Color.White)
        {

        }

        public TagsVisualizatorSettings(string filename, Size bitmapSize, Color backgroundColor)
            : this(filename, bitmapSize, backgroundColor, FontFamily.GenericSerif)
        {

        }

        public TagsVisualizatorSettings(string filename,
            Size bitmapSize,
            Color backgroundColor,
            FontFamily family,
            float minMargin = 10,
            bool fillTags = false)
        {
            Filename = filename;
            BitmapSize = bitmapSize;
            BackgroundColor = backgroundColor;
            FontFamily = family;
            MinMargin = minMargin;
            FillTags = fillTags;
        }
    }
}
