using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Drawers
{
    public abstract class WordDrawer
    {
        protected ImageSettings ImageSettings { get; }
        protected Font Font { get; }
        protected Palette Palette { get; }

        public WordDrawer(ImageSettings imageSettings, Font font, Palette palette)
        {
            ImageSettings = imageSettings;
            Font = font;
            Palette = palette;
        }

        public abstract Bitmap GetDrawnLayoutedWords(PaintedWord[] layoutedWords);
    }
}