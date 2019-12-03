using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    public class GrayTheme : Theme
    {
        private readonly ImmutableArray<Brush> rectangleBrushes = ImmutableArray.Create(Brushes.Gray);

        public override ImmutableArray<Brush> RectangleBrushes => rectangleBrushes;
        public override Brush BackgroundBrush => Brushes.White;
        public override Brush GetTagBrush(Tag tag) => Brushes.Black;
    }
}