using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Commands;

namespace TagsCloudVisualization.Common.Settings
{
    public class TagStyleSettings : ITagStyleSettings
    {
        public Color[] ForegroundColors { get; set; }
        public string[] FontFamilies { get; set; }
        public float Size { get; set; }
        public float SizeScatter { get; set; }

        public TagStyleSettings(CommandLineOptions options)
        {
            ForegroundColors = options.ForegroundColors
                .Select(color => ColorTranslator.FromHtml(color.Trim()))
                .ToArray();
            FontFamilies = options.Fonts.Select(font => font.Trim()).ToArray();
            Size = options.TagSize;
            SizeScatter = options.TagSizeScatter;
        }
    }
}