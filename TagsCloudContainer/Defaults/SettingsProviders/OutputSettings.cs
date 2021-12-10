using Mono.Options;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class OutputSettings : ICliSettingsProvider
{
    private static string outputPath = "image.png";
    private static ImageFormatType imageFormat = ImageFormatType.Png;

    public string OutputPath { get => outputPath; private set => outputPath = value; }
    public ImageFormatType ImageFormat { get => imageFormat; private set => imageFormat = value; }
    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"output=","Name of the output image. Defaults to 'image.png'",v=>OutputPath=v},
                {"image-format=","Format of the output image. Defaults to png", (ImageFormatType v)=>ImageFormat=v }
            };

        return options;
    }
}