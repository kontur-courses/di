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

        [Option("layout", Required = false, HelpText = "Set cloud layouter - Spiral or Random.")]
        public string Layout { get; set; }

        public static AppSettings ParseArgs(CommandLineOptions options)
        {
            var appSettings = new AppSettings();
            appSettings.DrawingSettings = new();

            appSettings.TextFile = options.Filename;
            appSettings.OutImagePath = options.Output;
            appSettings.FilterFile = options.Filter;

            appSettings.DrawingSettings.FontFamily = GetFontFamily(options.FontFamily);
            appSettings.DrawingSettings.FontSize = options.FontSize;
            appSettings.DrawingSettings.Size = options.Size;
            appSettings.DrawingSettings.Colors = GetColors(options.Colors);
            appSettings.DrawingSettings.PointsProvider = GetPointsProvider(
                options.Layout,
                appSettings.DrawingSettings.Size);
            return appSettings;
        }

        private static IPointsProvider GetPointsProvider(string layout, Size size)
        {
            var center = new Point(size.Width / 2, size.Height / 2);

            if (layout.ToLowerInvariant() == "random")
            {
                return new RandomPointsProvider(center);
            }

            return new SpiralPointsProvider(center);
        }

        private static FontFamily GetFontFamily(string fontName)
        {
            try
            {
                FontFamily fontFamily = new FontFamily(fontName);
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
            var c = new List<Color>();

            colors.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Color.FromName(x)).ToList();

            if (c.Count > 0)
            {
                return c.ToList();
            }

            return new List<Color>() { Color.White };
        }
    }
}
