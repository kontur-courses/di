using System.Drawing;
using Autofac;
using TagCloud.Common.Drawing;
using TagCloud.Common.Layouter;
using TagCloud.Common.Options;
using TagCloud.Common.Saver;
using TagCloud.Common.TagsConverter;
using TagCloud.Common.TextFilter;
using TagCloud.Common.WeightCounter;

namespace TagCloud.Common.DI;

public static class CloudContainerBuilder
{
    public static IContainer CreateContainer(VisualizationOptions visualizationOptions)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.Register(cc => visualizationOptions);
        containerBuilder.Register<IWeightCounter>(cc => new SimpleWeightCounter()).SingleInstance();
        containerBuilder.Register<ICloudLayouter>(cc
            => new CircularCloudLayouter(new Point(0, 0))).SingleInstance();
        containerBuilder.Register<ITagsConverter>(cc
            => new SimpleTagsConverter(cc.Resolve<ICloudLayouter>(), cc.Resolve<IWeightCounter>())).SingleInstance();
        containerBuilder.Register<ICloudDrawer>(cc => new CloudDrawer(cc.Resolve<VisualizationOptions>().DrawingOptions))
            .SingleInstance();
        containerBuilder.Register<ITextFilter>(cc => new SimpleTextFilter()).SingleInstance();
        containerBuilder.Register<ICloudSaver>(cc => new LocalCloudSaver(cc.Resolve<VisualizationOptions>().SavingOptions));
        containerBuilder.Register(cc
            => new TagCloudCreator(cc.Resolve<ICloudDrawer>(), cc.Resolve<ITagsConverter>(),
                cc.Resolve<ITextFilter>())).SingleInstance();
        return containerBuilder.Build();
    }
}