using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings.Interfaces
{
    public interface ISaveSettings
    {
        string OutputFile { get; }
        ImageFormat ImageFormat { get; }
    }
}