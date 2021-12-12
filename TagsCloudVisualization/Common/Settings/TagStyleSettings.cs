using System.Drawing;

namespace TagsCloudVisualization.Common.Settings
{
    public class TagStyleSettings : ITagStyleSettings
    {
        public Color[] ForegroundColors { get; set; }
        public string[] FontFamilies { get; set; }
        public float Size { get; set; }
        public float SizeScatter { get; set; }

        public TagStyleSettings()
        {
            ForegroundColors = new[] {Color.Chocolate};
            FontFamilies = new[] {"Arial"};
            Size = 40;
        }

        public TagStyleSettings(Color[] foregroundColors, string[] fontFamilies, float size, float sizeScatter)
        {
            ForegroundColors = foregroundColors;
            FontFamilies = fontFamilies;
            Size = size;
            SizeScatter = sizeScatter;
        }
    }
}