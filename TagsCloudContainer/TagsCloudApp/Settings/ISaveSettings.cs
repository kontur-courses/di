using System.Drawing.Imaging;

namespace TagsCloudApp.Settings
{
    public interface ISaveSettings
    {
        string OutputFile { get; }
        ImageFormat ImageFormat { get; }
    }
}