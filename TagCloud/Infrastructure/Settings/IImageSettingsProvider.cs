using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public interface IImageSettingsProvider : IFontSettingProvider
    {
        public Brush Brush { get; }
        public int Width { get; }
        public int Height { get; }
        public string ImagePath { get; }
    }
}