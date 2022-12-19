using System.ComponentModel;
using TagCloudContainer.Interfaces;
using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Settings
{
    public class AlgorithmSettings : IAlgorithmSettings
    {
        public AlgorithmSettings(IFontSettings fontSettings)
        {
            FontSettings = fontSettings;
        }

        [Browsable(false)] public IFontSettings FontSettings { get; set; }

        [Browsable(false)]
        public FontFamily Font
        {
            get => FontSettings.Font;
            set => FontSettings.Font = value;
        }

        [DisplayName("Максимальный шрифт")]
        public int MaxFont
        {
            get => FontSettings.MaxFont;
            set => FontSettings.MaxFont = value;
        }

        [DisplayName("Минимальный шрифт")]
        public int MinFont
        {
            get => FontSettings.MinFont;
            set => FontSettings.MinFont = value;
        }

        [DisplayName("Путь к файлу")] public string ImagesDirectory { get; set; }
    }
}
