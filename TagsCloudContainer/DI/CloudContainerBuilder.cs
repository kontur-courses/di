using System.Drawing;
using Autofac;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.TagsConverter;
using TagsCloudContainer.WeightCounter;

namespace TagsCloudContainer.DI;

public static class CloudContainerBuilder
{
    public static void Build(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register<IWeightCounter>(cc => new SimpleWeightCounter()).SingleInstance();
        containerBuilder.Register<ICloudLayouter>(cc
            => new CircularCloudLayouter(new Point(0, 0))).SingleInstance();
        containerBuilder.Register<ITagsConverter>(cc
            => new SimpleTagsConverter(cc.Resolve<ICloudLayouter>(), cc.Resolve<IWeightCounter>())).SingleInstance();
        containerBuilder.Register<ICloudDrawer>(cc => new CloudDrawer()).SingleInstance();
        containerBuilder.Register(cc
            => new TagCloudCreator(cc.Resolve<ICloudDrawer>(), cc.Resolve<ITagsConverter>())).SingleInstance();
    }
}