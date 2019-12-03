using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    public abstract class Theme
    {
        public abstract ImmutableArray<Brush> RectangleBrushes { get; }

        public abstract Brush BackgroundBrush { get; }

        public abstract Brush GetTagBrush(Tag tag);
        internal static SolidBrush GetSolidBrush(string hexColor) => new SolidBrush(ColorTranslator.FromHtml(hexColor));
    }
}