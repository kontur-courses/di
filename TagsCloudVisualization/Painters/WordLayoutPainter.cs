using System;
using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Painters
{
    public abstract class WordLayoutPainter
    {
        protected ImageSettings imageSettings { get; }
        protected Font font { get; }
        protected Palette palette { get; }

        public WordLayoutPainter(ImageSettings imageSettings, Font font, Palette palette)
        {
            this.imageSettings = imageSettings;
            this.font = font;
            this.palette = palette;
        }

        public abstract Bitmap GetDrawnLayoutedWords(LayoutedWord[] layoutedWords);
    }
}