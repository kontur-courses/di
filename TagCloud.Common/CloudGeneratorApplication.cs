using Autofac;
using TagCloud.Common.DI;
using TagCloud.Common.Options;
using TagCloud.Common.Saver;

namespace TagCloud.Common;

public class CloudGeneratorApplication
{
    public static void Run(VisualizationOptions visualizationOptions)
    {
        var container = CloudContainerBuilder.CreateContainer(visualizationOptions);
        var cloudCreator = container.Resolve<TagCloudCreator>();
        var cloudSaver = container.Resolve<ICloudSaver>();
        var cloud = cloudCreator.CreateCloud(visualizationOptions.WordsOptions);
        cloudSaver.SaveCloud(cloud);
    }
}