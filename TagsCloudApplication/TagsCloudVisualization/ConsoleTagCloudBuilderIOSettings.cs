using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public class ConsoleTagCloudBuilderIOSettings : ITagCloudBuilderProperties
    {
        public string InputFilename { get; private set; }
        public string OutputFilename { get; private set; }
        public ImageFormat OutputFormat { get; private set; }

        public ConsoleTagCloudBuilderIOSettings(Options options)
        {
            InputFilename = options.InputFilename;
            OutputFilename = options.OutputFilename;
            OutputFormat = ImageUtilities.GetFormatFromString(options.OutputImageFormatName);
        }
    }
}
