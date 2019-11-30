using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Painters
{
    public abstract class WordLayoutPainter
    {
        protected ImageSettings ImageSettings { get; }
        protected Font Font { get; }
        protected Palette Palette { get; }

        public WordLayoutPainter(ImageSettings imageSettings, Font font, Palette palette)
        {
            ImageSettings = imageSettings;
            Font = font;
            Palette = palette;
        }

        public abstract Bitmap GetDrawnLayoutedWords(LayoutedWord[] layoutedWords);
    }
}