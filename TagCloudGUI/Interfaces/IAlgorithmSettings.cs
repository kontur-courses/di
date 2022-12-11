using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.TagsWithFont;

namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface IAlgorithmSettings : IVisualizationCloudSettings, IProviderSettings
    {
        public IFontSettings FontSettings { get; set; }
        public IPointConfig PointConfig { get; set; }
        public Point StartPoint { get; set; }
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

        public double DensityMultiplier
        {
            get => PointConfig.DensityMultiplier;
            set => PointConfig.DensityMultiplier = value;
        }

        public double EllipsoidMultiplier
        {
            get => PointConfig.EllipsoidMultiplier;
            set => PointConfig.EllipsoidMultiplier = value;
        }


    }
}
