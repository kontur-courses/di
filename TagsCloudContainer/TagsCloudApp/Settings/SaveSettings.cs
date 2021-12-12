using System.Drawing.Imaging;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;

namespace TagsCloudApp.Settings
{
    public class SaveSettings : ISaveSettings
    {
        public string OutputFile { get; }
        public ImageFormat ImageFormat { get; }

        public SaveSettings(IRenderArgs renderArgs, IImageFormatParser imageFormatParser)
        {
            OutputFile = renderArgs.OutputPath;
            ImageFormat = imageFormatParser.Parse(renderArgs.ImageFormat);
        }
    }
}