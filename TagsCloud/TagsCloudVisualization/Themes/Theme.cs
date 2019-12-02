using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Themes
{
    public abstract class Theme
    {
        public abstract ImmutableArray<Brush> RectangleBrushes { get; }

        public abstract Brush BackgroundBrush { get; }

        internal static SolidBrush GetSolidBrush(string hexColor) => new SolidBrush(ColorTranslator.FromHtml(hexColor));
    }
}