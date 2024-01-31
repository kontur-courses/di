using CommandLine;
using System.Drawing;
using TagsCloudContainer.SettingsClasses;
using TagsCloudContainer.TagCloudBuilder;
using TagsCloudVisualization;

namespace TagsCloudContainer.CLI
{
    class CommandLineOptions
    {
        [Option('f', "filename", Required = true, HelpText = "Input file name.")]
        public string Filename { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file name.")]
        public string Output { get; set; }

        [Option("font", Required = false, HelpText = "Set the font family name.")]
        public string FontFamily { get; set; }

        [Option("fontsize", Required = false, HelpText = "Set the font size. Must be a positive integer.")]
        public int FontSize { get; set; }

        [Option("colors", Required = false, HelpText = "List of color names. Separated by commas.")]
        public string Colors { get; set; }

        [Option("size", Required = false, HelpText = "Set the image size. Must be two positive integer.")]
        public Size Size { get; set; }

        [Option("exclude", Required = false, HelpText = "File with words to exclude.")]
        public string Filter { get; set; }

        [Option("layout", Required = false, HelpText = "Set cloud layouter - Spiral, Random or Normal.")]
        public string Layout { get; set; }

        [Option("background", Required = false, HelpText = "Set background color.")]
        public string BgColor { get; set; }

        public static AppSettings ParseArgs(CommandLineOptions options)
        {
            var appSettings = new AppSettings();
            appSettings.DrawingSettings = new();

            appSettings.TextFile = options.Filename;
            appSettings.OutImagePath = options.Output;
            appSettings.FilterFile = options.Filter;

            appSettings.DrawingSettings.FontFamily = GetFontFamily(options.FontFamily);
            appSettings.DrawingSettings.FontSize = GetFontSize(options.FontSize);
            appSettings.DrawingSettings.Size = GetSize(options.Size);
            appSettings.DrawingSettings.Colors = GetColors(options.Colors);
            appSettings.DrawingSettings.PointsProvider = GetPointsProvider(
                options.Layout,
                appSettings.DrawingSettings.Size);
            appSettings.DrawingSettings.BgColor = GetBGColor(options.BgColor);

            return appSettings;
        }

        private static int GetFontSize(int fontSize)
        {
            if (fontSize > 0)
            {
                return fontSize;
            }
            return 12;
        }

        private static Size GetSize(Size size)
        {
            if (size.IsEmpty)
            {
                return new Size(800, 600);
            }
            return size;
        }

        private static IPointsProvider GetPointsProvider(string layout, Size size)
        {
            var center = new Point(size.Width / 2, size.Height / 2);
            IPointsProvider pointProvider = new SpiralPointsProvider();

            if (string.IsNullOrEmpty(layout))
            {
                pointProvider.Initialize(center);
                return pointProvider;
            }

            switch (layout.ToLowerInvariant())
            {
                case "random":
                    pointProvider = new RandomPointsProvider();
                    break;

                case "normal":
                    pointProvider = new NormalPointsProvider();
                    break;

                case "spiral":
                    pointProvider = new NormalPointsProvider();
                    break;

                default:
                    pointProvider = new SpiralPointsProvider();
                    break;
            }

            pointProvider.Initialize(center);
            return pointProvider;
        }

        private static FontFamily GetFontFamily(string fontName)
        {
            try
            {
                var fontFamily = new FontFamily(fontName);
                return fontFamily;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Font {0} not found. Using default font.", fontName);
                return new FontFamily("Arial");
            }
        }

        private static IList<Color> GetColors(string colors)
        {
            if (string.IsNullOrEmpty(colors))
            {
                return new List<Color>() { Color.White };
            }

            var c = colors.Split(',').Select(x => Color.FromName(x)).ToList();

            if (c.Count > 0)
            {
                return c.ToList();
            }

            return new List<Color>() { Color.White };
        }

        private static Color GetBGColor(string colorName)
        {
            return Color.FromName(colorName);
        }
    }
}