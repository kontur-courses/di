using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings
{
    public interface ISaveSettings
    {
        string OutputFile { get; }
        ImageFormat ImageFormat { get; }
    }
}