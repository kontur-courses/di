using System.Drawing;

namespace TagsCloudContainer.Themes
{
    public class Black : ITheme
    {
        public FontFamily FontFamily => new FontFamily("Arial");
        public Brush BackgroundColor => Brushes.Black;
        public Brush WordColor => Brushes.Red;
    }
}