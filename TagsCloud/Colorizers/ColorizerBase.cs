using SixLabors.ImageSharp;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

public abstract class ColorizerBase
{
    protected readonly IList<Color> colors;

    protected ColorizerBase(IList<Color> colors)
    {
        this.colors = colors;
    }

    public abstract void Colorize(IDictionary<CloudTag, int> frequencyStatistics);
}