using System.Drawing;

namespace TagsCloudContainer
{
    public class CloudDrawingSettings
    {
        public readonly FontFamily FontFamily;
        public readonly float FontSize;
        public readonly float MinFontSize;
        public readonly IList<Color> Colors;

        public CloudDrawingSettings(string fontFamily, float maxFontSize, IList<Color> colors)
        {
            try
            {
                FontFamily = new FontFamily(fontFamily);
            }
            catch
            {
                Console.WriteLine("Font '{0}' not found. Using default font.", fontFamily);
                FontFamily = new FontFamily("Arial");
            }

            FontSize = maxFontSize;
            Colors = colors;
        }
    }
}
