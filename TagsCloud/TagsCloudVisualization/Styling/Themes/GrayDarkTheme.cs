using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    public class GrayDarkTheme : ITheme
    {
        public string[] TextColors => new[]
        {
            "#424242",
            "#757575",
            "#BDBDBD",
            "#EEEEEE",
            "#FAFAFA",
        };

        public string BackgroundColor => "#000000";
        public Brush GetTagBrush(Tag tag) => Brushes.Black;
    }
}