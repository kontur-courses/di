using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public class Settings : IFileSettingsProvider, ITagCloudSettingsProvider, IExcludeTypesSettingsProvider, ISpiralSettingsProvider, IImageSettingsProvider, IWordCountThresholdSettingProvider
    {
        public string Path { get; set; }
        public Point Center { get; set; }
        public int Increment { get; set; }
        public string[] ExcludedTypes { get; set; }
        public FontFamily FontFamily { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        public Brush Brush { get; set; }
        public int Width { get; set;  }
        public int Height { get; set; }
        public string ImagePath { get; set; }
        public int WordCountThreshold { get; set;  }
    }
}