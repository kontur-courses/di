using System.Drawing;
using TagCloud.PointGenerator;
using TagCloud.Templates.Colors;

namespace TagCloud.Configurations
{
    public class Configuration
    {
        public Color BackgroundColor { get; set; } = Color.Aqua;
        public IColorGenerator ColorGenerator { get; set; } = new RandomColorGenerator();
        public FontFamily FontFamily { get; set; } = new("Arial");
        public Size ImageSize { get; set; }

        public IPointGenerator PointGenerator { get; set; }
        /*= new Circle(0.1f, 0.9, new(0, 0), new Cache());*/
            = new Spiral(2.5f, 25f, new Cache()); 

        public string WordsFilename { get; set; }
        public string OutputFilename { get; set; }

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