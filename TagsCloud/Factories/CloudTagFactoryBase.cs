using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Factories;

public abstract class CloudTagFactoryBase
{
    protected readonly IFactoryOptions options;
    protected readonly List<string> words;

    protected CloudTagFactoryBase(IFactoryOptions options, List<string> words)
    {
        this.options = options;
        this.words = words;
    }

    public abstract CloudTagFactoryBase AdjustFonts();
    public abstract CloudTagFactoryBase AdjustColors();
    public abstract CloudTagFactoryBase AdjustPositions();
    public abstract List<CloudTag> Build();
}