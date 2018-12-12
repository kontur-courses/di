using System.Drawing;
using System.Runtime.InteropServices;
using CommandLine;
using TagCloud.Core.Settings;
using TagCloud.Core.Util;

namespace TagCloud.CUI
{
    public class CommandLineOptions
    {
        #region TagCloudSettings
        [Option('p', "pathtowords", Required = true)]
        public string PathToWords { get; set; }

        [Option('b', "pathtoboringwords")]
        public string PathToBoringWords { get; set; }

        [Option('i', "imagepath")]
        public string PathForResultImage { get; set; }
        #endregion

        #region PaintingSettings
        [Option('b', "backgroundcolor")]
        public string BackgroundColorName { get; set; }

        [Option("tagbrush")]
        public string TagBrushName { get; set; }
        #endregion

        #region TextWorkingSettings
        [Option('c', "maxtagscount")]
        public int? MaxTagsCount { get; set; }
        #endregion

        #region VisualizingSettings
        [Option('w', "width")]
        public int? Width { get; set; }

        [Option('h', "height")]
        public int? Height { get; set; }

        [Option('f', "font")]
        public string FontName { get; set; }

        [Option("minfontsize")]
        public int? MinFontSize { get; set; }

        [Option("maxfontsize")]
        public int? MaxFontSize { get; set; }
        #endregion

        #region LayoutingSettings
        [Option("spiralstep")]
        public double? SpiralStepMultiplier { get; set; }
        #endregion

        public void UpdateSettings(params ISettings[] all_settings)
        {
            foreach (var settings in all_settings)
            {
                switch (settings)
                {
                    case LayoutingSettings layoutingSettings:
                        UpdateLayoutingSettings(layoutingSettings);
                        break;
                    case PaintingSettings paintingSettings:
                        UpdatePaintingSettings(paintingSettings);
                        break;
                    case TagCloudSettings tagCloudSettings:
                        UpdateTagCloudSettings(tagCloudSettings);
                        break;
                    case TextWorkingSettings textWorkingSettings:
                        UpdateTextWorkingSettings(textWorkingSettings);
                        break;
                    case VisualizingSettings visualizingSettings:
                        UpdateVisualizingSettings(visualizingSettings);
                        break;
                }
            }
        }

        private void UpdatePaintingSettings(PaintingSettings settings)
        {
            if (BackgroundColorName != null)
                settings.BackgroundColor = Color.FromName(BackgroundColorName);
            if (TagBrushName != null)
                settings.TagColor = Color.FromName(TagBrushName);
        }

        private void UpdateTextWorkingSettings(TextWorkingSettings settings)
        {
            settings.MaxUniqueWordsCount = MaxTagsCount;
        }

        private void UpdateVisualizingSettings(VisualizingSettings settings)
        {
            if (MaxFontSize.HasValue)
                settings.MaxFontSize = MaxFontSize.Value;
            if (MinFontSize.HasValue)
                settings.MinFontSize = MinFontSize.Value;
            if (FontName != null)
                settings.FontName = FontName;
            if (Width.HasValue)
                settings.Width = Width.Value;
            if (Height.HasValue)
                settings.Height = Height.Value;
        }

        private void UpdateLayoutingSettings(LayoutingSettings settings)
        {
            if (SpiralStepMultiplier.HasValue)
                settings.SpiralStepMultiplier = SpiralStepMultiplier.Value;
        }

        private void UpdateTagCloudSettings(TagCloudSettings settings)
        {
            settings.PathToWords = PathToWords;
            if (PathToBoringWords != null)
                settings.PathToBoringWords = PathToBoringWords;
            if (PathForResultImage != null)
                settings.PathForResultImage = PathForResultImage;
        }
    }
}