using TagCloudContainer.Interfaces;

namespace TagCloudGUI.Interfaces
{
    public interface IAlgorithmSettings : IVisualizationCloudSettings, IProviderSettings
    {
        IFontSettings FontSettings { get; set; }
        string ImagesDirectory { get; set; }

        FontFamily Font
        {
            get => FontSettings.Font;
            set => FontSettings.Font = value;
        }

        int MaxFont
        {
            get => FontSettings.MaxFontSize;
            set => FontSettings.MaxFontSize = value;
        }

        int MinFont
        {
            get => FontSettings.MinFontSize;
            set => FontSettings.MinFontSize = value;
        }
    }
}
