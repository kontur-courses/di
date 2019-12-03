using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudTagProperties
    {
        public readonly FontFamily TextFontFamily;
        public readonly int MinSize;

        public CloudTagProperties(FontFamily fontFamily, int minSize)
        {
            TextFontFamily = fontFamily;
            MinSize = minSize;
        }
    }
}
