using Autofac;
using System.Reflection;
using System.Windows;
using TagsCloud;
using TagsCloud.Renderers;
using TagsCloud.FileParsers;
using TagsCloud.ImageSavers;
using TagsCloud.Layouters;
using TagsCloud.WordsFiltering;

namespace TagsCloud_wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            var tagsCloudAssembly = Assembly.GetAssembly(typeof(TagsCloudGenerator));
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFileParser).Namespace).As<IFileParser>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFilter).Namespace).As<IFilter>().SingleInstance().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(ITagsCloudLayouter).Namespace).As<ITagsCloudLayouter>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(ITagsCloudRenderer).Namespace).As<ITagsCloudRenderer>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IImageSaver).Namespace).As<IImageSaver>().SingleInstance();
            builder.RegisterType<TagsCloudGenerator>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();  

            var container = builder.Build();
            var window = container.Resolve<MainWindow>();
            window.Show();            
        }
    }
}
