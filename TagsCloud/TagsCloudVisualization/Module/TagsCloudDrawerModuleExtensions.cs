using Autofac;

namespace TagsCloudVisualization.Module
{
    public static class TagsCloudDrawerModuleExtensions
    {
        public static ContainerBuilder RegisterTagsClouds(this ContainerBuilder builder,
            TagsCloudDrawerModuleSettings settings)
        {
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            return builder;
        }
    }
}