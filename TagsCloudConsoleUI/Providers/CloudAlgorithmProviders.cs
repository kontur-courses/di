using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudConsoleUI.Providers;

public static class CloudAlgorithmProviders
{
    public static readonly IReadOnlyDictionary<string, ICloudLayouter> RegisteredProviders = 
        new Dictionary<string, ICloudLayouter>
    {
        {"Circular", new CircularCloudLayouter(Point.Empty)}
    };
}