using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Themes
{
    public class RedTheme : Theme
    {
        private readonly ImmutableArray<Brush> rectangleBrushes = ImmutableArray.Create<Brush>(
            GetSolidBrush("#E57373"),
            GetSolidBrush("#F44336"),
            GetSolidBrush("#D32F2F"),
            GetSolidBrush("#B71C1C"));

        public override ImmutableArray<Brush> RectangleBrushes => rectangleBrushes;

        public override Brush BackgroundBrush => GetSolidBrush("#FFCDD2");
    }
}