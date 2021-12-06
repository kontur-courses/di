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
        public Action<IReadOnlyList<ITag>> SetTagPaletteByColorIndex { get; }

        public TagsVisualizatorSettings(string filename) : 
            this(filename, new Size(800,800))
        {

        }

        public TagsVisualizatorSettings(string filename, Size bitmapSize) :
            this(filename, bitmapSize, new List<Color> {Color.DarkGreen})
        {

        }

        public TagsVisualizatorSettings(string filename, Size bitmapSize, List<Color> textColors) :
            this(filename, bitmapSize, textColors, null)
        {

        }

        public TagsVisualizatorSettings
            (string filename, Size bitmapSize, List<Color> textColors, List<Color> tagsColors) :
            this(filename, bitmapSize, textColors, tagsColors, Color.White)
        {

        }

        public TagsVisualizatorSettings(string filename,
                Size bitmapSize,
                List<Color> textColors,
                List<Color> tagsColors,
                Color backgroundColor) : this
            (filename, bitmapSize, textColors, tagsColors, backgroundColor, FontFamily.GenericSerif)
        {

        }

        public TagsVisualizatorSettings(string filename,
            Size bitmapSize,
            List<Color> textColors,
            List<Color> tagsColors,
            Color backgroundColor,
            FontFamily family,
            float minMargin = 10,
            Action<IReadOnlyList<ITag>> setTagPaletteByColorIndex = null)
        {
            Filename = filename;
            BitmapSize = bitmapSize;
            BackgroundColor = backgroundColor;
            FontFamily = family;
            MinMargin = minMargin;
            FillTags = tagsColors != null && tagsColors.Count > 0;
            setTagPaletteByColorIndex ??= SetTagPaletteByColorIndexDefaultFunc(textColors, tagsColors);
            SetTagPaletteByColorIndex = setTagPaletteByColorIndex;

        }

        private Action<IReadOnlyList<ITag>> SetTagPaletteByColorIndexDefaultFunc
            (List<Color> textColors, List<Color> tagsColors)
        {
            if (!FillTags)
                return (tags) =>
                {
                    var i = 0;
                    foreach (var tag in tags)
                    {
                        tag.Palette = new Palette(textColors[i]);
                        i = (i + 1) % textColors.Count;
                    }
                };
            return (tags) =>
            {
                var i = 0;
                var k = 0;
                foreach (var tag in tags)
                {
                    tag.Palette = new Palette(textColors[i], tagsColors[k]);
                    i = (i + 1) % textColors.Count;
                    k = (k + 1) % tagsColors.Count;
                }
            };
        }
    }
}
