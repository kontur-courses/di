using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Themes
{
    public class GreenTheme : Theme
    {
        private readonly ImmutableArray<Brush> rectangleBrushes = ImmutableArray.Create<Brush>(
            GetSolidBrush("#81C784"),
            GetSolidBrush("#4CAF50"),
            GetSolidBrush("#388E3C"),
            GetSolidBrush("#1B5E20"));
        
        public override ImmutableArray<Brush> RectangleBrushes => rectangleBrushes;

        public override Brush BackgroundBrush => GetSolidBrush("#C8E6C9");
    }
}