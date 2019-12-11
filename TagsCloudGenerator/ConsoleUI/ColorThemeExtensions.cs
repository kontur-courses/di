using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudGenerator.ConsoleUI
{
    public static class ColorThemeExtensions
    {
        private static readonly Dictionary<ColorTheme, Palette> ColorThemes = new Dictionary<ColorTheme, Palette>()
        {
            [ColorTheme.First] = new Palette(),
            [ColorTheme.Second] = new Palette() {BackgroundColor = Color.Blue, PrimaryColor = Color.Yellow},
            [ColorTheme.Third] = new Palette() {BackgroundColor = Color.LightSlateGray, PrimaryColor = Color.Coral}
        };

        public static Palette Palette(this ColorTheme theme)
        {
            return ColorThemes[theme];
        }
    }
}