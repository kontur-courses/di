using Autofac;
using TagsCloud.FileParsers;
using TagsCloud.ImageSavers;
using TagsCloud.Layouters;
using TagsCloud.Renderers;
using TagsCloud.WordsFiltering;

namespace TagsCloud.DI
{
    public class TagsCloudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var tagsCloudAssembly = System.Reflection.Assembly.GetAssembly(typeof(TagsCloudGenerator));
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFileParser).Namespace).As<IFileParser>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFilter).Namespace).As<IFilter>().SingleInstance().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(ITagsCloudLayouter).Namespace).As<ITagsCloudLayouter>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(ITagsCloudRenderer).Namespace).As<ITagsCloudRenderer>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IImageSaver).Namespace).As<IImageSaver>().SingleInstance();
            builder.RegisterType<WordsLoader>().AsSelf().SingleInstance();
            builder.RegisterType<WordsFilterer>().AsSelf().SingleInstance();
            builder.RegisterType<ImageSaveHelper>().AsSelf().SingleInstance();
            builder.RegisterType<TagsCloudGenerator>().AsSelf().SingleInstance();
        }
    }
}
