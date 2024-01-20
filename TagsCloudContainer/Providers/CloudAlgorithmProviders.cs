using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudContainer.Providers;

public static class CloudAlgorithmProviders
{
    public static readonly IReadOnlyDictionary<string, Action<ContainerBuilder>> RegisteredProviders = 
        new Dictionary<string, Action<ContainerBuilder>>
    {
        {"Circular", builder => builder.RegisterInstance(new CircularCloudLayouter(Point.Empty)).As<ICloudLayouter>().SingleInstance()}
    };
}