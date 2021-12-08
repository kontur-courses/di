using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudCLI
{
    public class SettingProvider
    {
        public Settings GetSettings(Options options) => new()
        {
            Directory = options.Directory,
            ImageName = options.ImageName,
            FontFamilyName = options.FontFamilyName,
            TagColor = Color.FromName(options.TagColor),
            StartPoint = new Point(0, 0),
            FileWithWords = options.FileWithWords,
            MaxFontSize = options.MaxFontSize,
            BoringWords = new[] { "в", "что", "не", "и", "с", "на", "то", "а", "он", "его", "для", "из" }
        };
    }
}