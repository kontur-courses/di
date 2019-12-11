using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    /// <summary>
    /// This theme uses endesga pixel art colors
    /// </summary>
    public class PixelArtTheme : ITheme
    {
        public string[] TextColors => new[]
        {
            "#e4a672",
            "#b86f50",
            "#743f39",
            "#3f2832",
            "#9e2835",
            "#e53b44",
            "#fb922b",
            "#ffe762",
            "#63c64d",
            "#327345",
            "#193d3f",
            "#4f6781",
            "#afbfd2",
            "#2ce8f4",
            "#0484d1"
        };

        public string BackgroundColor => "#ffffff";
    }
}