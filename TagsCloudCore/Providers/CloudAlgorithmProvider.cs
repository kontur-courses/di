using System.Drawing;
using TagsCloudCore.Common.Enums;
using TagsCloudVisualization;

namespace TagsCloudCore.Providers;

public static class CloudAlgorithmProvider
{
    public static readonly IReadOnlyDictionary<CloudBuildingAlgorithm, ICloudLayouter> RegisteredProviders =
        new Dictionary<CloudBuildingAlgorithm, ICloudLayouter>
        {
            {CloudBuildingAlgorithm.Circular, new CircularCloudLayouter(Point.Empty)}
        };
}