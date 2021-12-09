using System.Drawing.Imaging;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class SaveSettings : ISaveSettings
    {
        public string OutputFile { get; }
        public ImageFormat ImageFormat { get; }

        public SaveSettings(IRenderOptions renderOptions, IImageFormatParser imageFormatParser)
        {
            OutputFile = renderOptions.OutputPath;
            ImageFormat = imageFormatParser.Parse(renderOptions.ImageFormat);
        }
    }
}