using System.Drawing;
using TagCloud.Enums;

namespace TagCloudCreator
{
    public class Configuration
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string StopWordsFile { get; set; }
        public Color BackgroundColor { get; set; }
        public Size ImageSize { get; set; }
        public CloudLayouterType LayouterType { get; set; }
        public ColorScheme ColorScheme { get; set; }
        public FontScheme FontScheme { get; set; }
        public SizeScheme SizeScheme { get; set; }
    }
}