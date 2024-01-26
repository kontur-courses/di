using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Builders;

public abstract class CloudTagListBuilderBase
{
    protected readonly IFactoryOptions options;
    protected readonly List<WordToStatus> words;

    protected CloudTagListBuilderBase(IFactoryOptions options, List<WordToStatus> words)
    {
        this.options = options;
        this.words = words;
    }

    public abstract CloudTagListBuilderBase AdjustFonts();
    public abstract CloudTagListBuilderBase AdjustColors();
    public abstract CloudTagListBuilderBase AdjustPositions();
    public abstract List<CloudTag> Build();
}