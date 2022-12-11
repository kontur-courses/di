using System.ComponentModel;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Settings
{
    public class AlgorithmSettings : IAlgorithmSettings
    {
        public AlgorithmSettings(IFontSettings fontSettings, IPointConfig config)
        {
            FontSettings = fontSettings;
            PointConfig = config;
        }

        [Browsable(false)] public IFontSettings FontSettings { get; set; }
        [Browsable(false)] public IPointConfig PointConfig { get; set; }

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

        [DisplayName("Плотность(множитель)")]
        public double DensityMultiplier
        {
            get => PointConfig.DensityMultiplier;
            set => PointConfig.DensityMultiplier = value;
        }

        [DisplayName("Вертикальное сжатие облака(множитель)")]
        public double EllipsoidMultiplier
        {
            get => PointConfig.EllipsoidMultiplier;
            set => PointConfig.EllipsoidMultiplier = value;
        }

        [DisplayName("Смещение центра")] public Point StartPoint { get; set; }

        [DisplayName("Путь к файлу")] public string ImagesDirectory { get; set; }

    }
}
