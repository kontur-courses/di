namespace TagsCloudContainer.DrawingOptions;

public class DefaultOptionsProvider : IOptionsProvider
{
    public Options Options { get; }
    
    public DefaultOptionsProvider(Options options)
    {
        Options = options;
    }
}