using System.Drawing;
using TagCloud.PointGenerator;
using TagCloud.Templates.Colors;

namespace TagCloud.Configurations
{
    public class Configuration
    {
        public Color BackgroundColor { get; set; }
        public IColorGenerator ColorGenerator { get; set; } = new RandomColorGenerator();
        public FontFamily FontFamily { get; set; } = new("Arial");
        public static float MaxFontSize => 70;
        public static float MinFontSize => 20;
        public Size ImageSize { get; set; }
        public IPointGenerator PointGenerator { get; set; } = Circle.GetDefault();
        public string WordsFilename { get; }
        public string OutputFilename { get; }

        public Configuration(Color backgroundColor, IColorGenerator colorGenerator, Size imageSize,
            IPointGenerator pointGenerator, string wordsFilename, string outputFilename, FontFamily fontFamily)
        {
            BackgroundColor = backgroundColor;
            ColorGenerator = colorGenerator;
            ImageSize = imageSize;
            PointGenerator = pointGenerator;
            WordsFilename = wordsFilename;
            OutputFilename = outputFilename;
            FontFamily = fontFamily;
        }

        public Configuration(string wordsFilename, string outputFilename)
        {
            WordsFilename = wordsFilename;
            OutputFilename = outputFilename;
        }
    }
}