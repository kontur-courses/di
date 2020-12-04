using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.Settings
{
    public class AppSettings
    {
        public static readonly string DefaultInputFile = Path.Combine(
            Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt");

        public static readonly string DefaultOutputDirectory = Path.Combine(
            Directory.GetCurrentDirectory(), "..", "..", "..");

        public static AppSettings Default = new AppSettings(FontSettings.Default, ImageFormat.Bmp,
            ImageSettings.Default, DefaultInputFile, CloudLayouterAlgorithm.CircularCloudLayouter, Palette.Default,
            DefaultOutputDirectory);

        private AppSettings(FontSettings fontSettings, ImageFormat format,
            ImageSettings imageSettings, string inputFileName, CloudLayouterAlgorithm layouterAlgorithm,
            Palette palette, string outputDirectory)
        {
            FontSettings = fontSettings;
            Format = format;
            ImageSettings = imageSettings;
            InputFileName = inputFileName;
            LayouterAlgorithm = layouterAlgorithm;
            Palette = palette;
            OutputDirectory = outputDirectory;
        }

        public FontSettings FontSettings { get; set; }
        public ImageFormat Format { get; set; }
        public ImageSettings ImageSettings { get; set; }
        public string InputFileName { get; set; }
        public CloudLayouterAlgorithm LayouterAlgorithm { get; set; }
        public Palette Palette { get; set; }
        public string OutputDirectory { get; set; }
    }
}