using System.Drawing;

namespace TagsCloudContainer.Themes
{
    public class Classic : ITheme
    {
        public FontFamily FontFamily => new FontFamily("Times New Roman");
        public Brush BackgroundColor => Brushes.White;
        public Brush WordColor => Brushes.Black;
    }
}