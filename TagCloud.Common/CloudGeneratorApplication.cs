using Autofac;
using TagCloud.Common.DI;
using TagCloud.Common.Options;

namespace TagCloud.Common;

public class CloudGeneratorApplication
{
    public static void Run(VisualizationOptions visualizationOptions)
    {
        var container = CloudContainerBuilder.CreateContainer();
        var cloudCreator = container.Resolve<TagCloudCreator>();
        cloudCreator.CreateCloud(visualizationOptions);
    }
}