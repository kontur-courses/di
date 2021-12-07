using System.Drawing.Imaging;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class SaveSettings : ISaveSettings
    {
        public string OutputFile { get; }
        public ImageFormat ImageFormat { get; }

        public SaveSettings(string outputFile, ImageFormat imageFormat)
        {
            OutputFile = outputFile;
            ImageFormat = imageFormat;
        }
    }
}