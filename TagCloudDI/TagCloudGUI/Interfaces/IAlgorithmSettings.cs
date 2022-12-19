using TagCloudContainer.Interfaces;

namespace TagCloudGUI.Interfaces
{
    public interface IAlgorithmSettings : IVisualizationCloudSettings, IProviderSettings
    {
        public IFontSettings FontSettings { get; set; }
        public string ImagesDirectory { get; set; }

        public FontFamily Font
        {
            get => FontSettings.Font;
            set => FontSettings.Font = value;
        }

        public int MaxFont
        {
            get => FontSettings.MaxFont;
            set => FontSettings.MaxFont = value;
        }

        public int MinFont
        {
            get => FontSettings.MinFont;
            set => FontSettings.MinFont = value;
        }
    }
}
