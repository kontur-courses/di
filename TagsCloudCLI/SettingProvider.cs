using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudCLI
{
    public class SettingProvider
    {
        public Settings GetSettings() => new()
        {
            Directory = "ImageExamples",
            ImageName = "example.bmp",
            FontFamilyName = "Arial",
            TagColor = Color.Firebrick,
            StartPoint = new Point(0, 0),
            FileWithWords = "text.txt",
            MaxFontSize = 100,
            BoringWords = new[] { "в", "что", "не", "и", "с", "на", "то", "а", "он", "его", "для", "из" }
        };
    }
}