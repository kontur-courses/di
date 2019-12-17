using System.Drawing;

namespace TagsCloudGenerator
{
    public class CloudFormat
    {
        public readonly ICloudColorPainter ColorPainter;
        public readonly ITagOrder TagOrderPreform;
        public readonly ITagTextPreform TagTextPreform;
        public readonly int FontSizeMultiplier;
        public readonly int MaximalFontSize;
        public readonly FontFamily TagTextFontFamily;

        public readonly StringFormat TagTextFormat;

        public CloudFormat(ICloudColorPainter colorPainter, ITagOrder tagOrderPreform, ITagTextPreform tagTextPreform,
            int fontSizeMultiplier, string tagTextFontFamily, int maximalFontSize, StringFormat tagTextFormat = null)
        {
            ColorPainter = colorPainter;
            TagOrderPreform = tagOrderPreform;
            TagTextPreform = tagTextPreform;
            FontSizeMultiplier = fontSizeMultiplier;
            MaximalFontSize = maximalFontSize;
            TagTextFontFamily = new FontFamily(tagTextFontFamily);
            TagTextFormat = tagTextFormat ?? new StringFormat
                                {FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.None};
        }
    }
}