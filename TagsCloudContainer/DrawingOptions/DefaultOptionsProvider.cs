using System.Drawing;

namespace TagsCloudContainer.DrawingOptions;

public class DefaultOptionsProvider : IOptionsProvider
{
    public DefaultOptionsProvider(Options options)
    {
        Options = options;
    }

    public Options Options { get; }
}