using System.Drawing;

namespace TagsCloudForm.Common
{
    public class Palette : IPalette
    {
        public Color PrimaryColor { get; set; } = Color.Black;
        public Color SecondaryColor { get; set; } = Color.GreenYellow;
        public Color BackgroundColor { get; set; } = Color.Gainsboro;
    }
}