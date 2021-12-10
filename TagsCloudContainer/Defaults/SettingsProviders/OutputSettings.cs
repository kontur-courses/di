using Mono.Options;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class OutputSettings : ICliSettingsProvider
{
    public string OutputPath { get; private set; } = "image.png";
    public ImageFormatType ImageFormat { get; private set; } = ImageFormatType.Png;
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