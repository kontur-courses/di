using SixLabors.ImageSharp;
using TagsCloudVisualization;

namespace TagsCloud.Colorizers;

public abstract class ColorizerBase
{
    protected readonly Color[] colors;

    protected ColorizerBase(Color[] colors)
    {
        this.colors = colors;
    }

    public abstract void Colorize(Dictionary<CloudTag, int> frequencyStatistics);
}