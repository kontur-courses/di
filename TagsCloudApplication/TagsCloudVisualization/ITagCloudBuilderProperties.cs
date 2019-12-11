using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public interface ITagCloudBuilderProperties
    {
        string InputFilename { get; }
        string OutputFilename { get; }
        ImageFormat OutputFormat { get; }
    }
}
