using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Settings
{
    public class Settings :
        IFileSettingsProvider,
        IExcludeTypesSettingsProvider,
        ISpiralSettingsProvider,
        IImageSettingsProvider,
        IWordCountThresholdSettingProvider,
        IImageFormatSettingProvider
    {
        public WordType[] ExcludedTypes { get; set; }
        public string Path { get; set; }
        public FontFamily FontFamily { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImagePath { get; set; }
        public Point Center { get; set; }
        public int Increment { get; set; }
        public int WordCountThreshold { get; set; }
        public ImageFormat Format { get; set; }

        public void Import(Settings settings)
        {
            ExcludedTypes = settings.ExcludedTypes;
            Path = settings.Path;
            WordCountThreshold = settings.WordCountThreshold;
            Increment = settings.Increment;
            Width = settings.Width;
            Height = settings.Height;
            MinFontSize = settings.MinFontSize;
            MaxFontSize = settings.MaxFontSize;
            Center = settings.Center;
            ImagePath = settings.ImagePath;
            FontFamily = settings.FontFamily;
            Format = settings.Format;
        }
    }
}