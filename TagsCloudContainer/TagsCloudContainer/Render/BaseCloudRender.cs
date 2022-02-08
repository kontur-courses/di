using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render;

public abstract class BaseCloudRender<TOptions> : ICloudRender where TOptions : CloudRenderOptions
{
    protected TOptions Options { get; }

    protected BaseCloudRender(TOptions options)
    {
        Options = options;
    }

    public abstract Image Render((string Word, int Count)[] words);
}