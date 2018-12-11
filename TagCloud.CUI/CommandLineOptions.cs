using System.Drawing;
using System.Runtime.InteropServices;
using CommandLine;
using TagCloud.Core.Settings;
using TagCloud.Core.Util;

namespace TagCloud.CUI
{
    public class CommandLineOptions
    {
        #region PaintingSettings
        [Option('b', "backgroundcolor")]
        public string BackgroundColorName { get; set; }

        [Option("tagbrush")]
        public string TagBrushName { get; set; }
        #endregion

        #region TextWorkingSettings
        [Option('p', "pathtowords", Required = true)]
        public string PathToWords { get; set; }

        [Option("pathtoboringwords")]
        public string PathToBoringWords { get; set; }

        [Option('c', "maxtagscount")]
        public int? MaxTagsCount { get; set; }
        #endregion

        #region VisualizingSettings
        [Option('w', "width")]
        public int? Width { get; set; }

        [Option('h', "height")]
        public int? Height { get; set; }

        [Option('i', "imagepath")]
        public string PathForResultImage { get; set; }

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

        public void UpdatePaintingSettings(PaintingSettings settings)
        {
            if (BackgroundColorName != null)
                settings.BackgroundColor = Color.FromName(BackgroundColorName);
            if (TagBrushName != null)
                settings.TagBrush = new SolidBrush(Color.FromName(TagBrushName));
        }

        public void UpdateTextWorkingSettings(TextWorkingSettings settings)
        {
            settings.PathToWords = PathToWords;
            settings.MaxUniqueWordsCount = MaxTagsCount;
            if (PathToBoringWords != null)
                settings.PathToBoringWords = PathToBoringWords;
        }

        public void UpdateVisualizingSettings(VisualizingSettings settings)
        {
            if (MaxFontSize.HasValue)
                settings.MaxFontSize = MaxFontSize.Value;
            if (MinFontSize.HasValue)
                settings.MinFontSize = MinFontSize.Value;
            if (FontName != null)
                settings.FontName = FontName;
            if (PathForResultImage != null)
                settings.PathForResultImage = PathForResultImage;
            if (Width.HasValue)
                settings.Width = Width.Value;
            if (Height.HasValue)
                settings.Height = Height.Value;
        }

        public void UpdateLayoutingSettings(LayoutingSettings settings)
        {
            if (SpiralStepMultiplier.HasValue)
                settings.SpiralStepMultiplier = SpiralStepMultiplier.Value;
        }
    }
}