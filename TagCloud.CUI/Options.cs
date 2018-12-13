using CommandLine;
using System.Drawing;
using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.CUI
{
    public class Options : ILayoutingSettings, IPaintingSettings, ITagCloudSettings, IVisualizingSettings, ITextParsingSettings
    {
        #region LayotingSettings
        [Option("spiralstep", Default = 1e-2)]
        public double SpiralStepMultiplier { get; set; }
        #endregion


        #region PaintingSettings
        private Color tagColor;
        private string tagBrushName;
        private string backgroundColorName;

        public Color BackgroundColor { get; set; }
        public Brush TagBrush { get; private set; }
        public Color TagColor
        {
            get => tagColor;
            set
            {
                tagColor = value;
                TagBrush = new SolidBrush(value);
            }
        }

        [Option("backgroundcolor", Default = "white")]
        public string BackgroundColorName
        {
            get => backgroundColorName;
            set
            {
                BackgroundColor = Color.FromName(value);
                backgroundColorName = value;
            }
        }

        [Option("tagbrush", Default = "black")]
        public string TagBrushName
        {
            get => tagBrushName;
            set
            {
                TagColor = Color.FromName(value);
                tagBrushName = value;
            }
        }
        #endregion


        #region TagCloudSettings
        [Option('p', "pathtowords", Required = true)]
        public string PathToWords { get; set; }

        [Option('b', "pathtoboringwords")] public string PathToBoringWords { get; set; }

        [Option('i', "imagepath", Default = "result.png")]
        public string PathForResultImage { get; set; }
        #endregion


        #region TextParsingOptions
        [Option('c', "maxtagscount")]
        public int? MaxUniqueWordsCount { get; set; }
        #endregion


        #region VisualizingOptions
        [Option('w', "width", Default = 800)]
        public int Width { get; set; } = 800;

        [Option('h', "height", Default = 600)]
        public int Height { get; set; } = 600;

        [Option('f', "font", Default = "arial")]
        public string FontName { get; set; } = "arial";

        [Option("minfontsize", Default = 15)]
        public float MinFontSize { get; set; } = 15;

        [Option("maxfontsize", Default = 35)]
        public float MaxFontSize { get; set; } = 35;

        public PointF CenterPoint => new PointF((float)Width / 2, (float)Height / 2);
        public Font DefaultFont => new Font(FontName, (MaxFontSize + MinFontSize) / 2);
        #endregion
    }
}