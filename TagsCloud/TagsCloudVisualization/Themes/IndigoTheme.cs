using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Themes
{
    public class IndigoTheme : Theme
    {
        private readonly ImmutableArray<Brush> rectangleBrushes = ImmutableArray.Create<Brush>(
            GetSolidBrush("#7986CB"),
            GetSolidBrush("#3F51B5"),
            GetSolidBrush("#303F9F"),
            GetSolidBrush("#1A237E"));

        public override ImmutableArray<Brush> RectangleBrushes => rectangleBrushes;
        
        public override Brush BackgroundBrush => GetSolidBrush("#C5CAE9");
    }
}