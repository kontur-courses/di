namespace TagCloud.Infrastructure.Settings
{
    public interface IImageSettingsProvider : IFontSettingProvider
    {
        public int Width { get; }
        public int Height { get; }
        public string ImagePath { get; }
    }
}