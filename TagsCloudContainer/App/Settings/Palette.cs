using System.Drawing;

namespace TagsCloudContainer.App.Settings
{
    public class Palette
    {
        public static readonly Palette Default = new Palette(Color.White, Color.Black);

        private Palette(Color textColor, Color backgroundColor)
        {
            TextColor = textColor;
            BackgroundColor = backgroundColor;
        }

        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}