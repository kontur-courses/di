using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings
{
    public interface ISaveSettings
    {
        string OutputFile { get; set; }
        ImageFormat ImageFormat { get; set; }
    }

    public class DefaultSaveSettings : ISaveSettings
    {
        public string OutputFile { get; set; } = "output.png";
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;
    }
}