using Autofac;
using TagsCloudDrawer.ImageCreator;
using TagsCloudVisualization.Drawable.Displayer;

namespace TagsCloudVisualization.Module
{
    public static class TagsCloudDrawerModuleExtensions
    {
        public static ContainerBuilder RegisterTagsClouds(this ContainerBuilder builder,
            TagsCloudVisualisationSettings settings)
        {
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            return builder;
        }

        public static ContainerBuilder RegisterImageCreation(this ContainerBuilder builder, string filename)
        {
            builder.Register(c => new ImageCreationDisplayer(filename, c.Resolve<IImageCreator>()))
                   .As<IDrawableDisplayer>();
            return builder;
        }
    }
}