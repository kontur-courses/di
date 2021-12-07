using Mono.Options;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class OutputSettings : ICliSettingsProvider
{
    public string OutputPath { get; set; } = "image.png";

    public ImageFormatType ImageFormat { get; set; } = ImageFormatType.Png;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"o=|output=","Name of the output image. Defaults to 'image.png'",v=>OutputPath=v},
                {"image-format=","Format of the output image. Defaults to png", (ImageFormatType v)=>ImageFormat=v }
            };

        return options;
    }
}