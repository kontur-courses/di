using System.Reflection;
using Autofac;
using TagsCloud.CloudPainter;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;
using TagsCloud.Settings;

namespace TagsCloud;

public static class ProgramConstructor
{
    public static IContainer ConstructProgram()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => typeof(IUiAction).IsAssignableFrom(t))
            .AsImplementedInterfaces();
        containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => typeof(IParser).IsAssignableFrom(t))
            .AsImplementedInterfaces();
        containerBuilder.RegisterType<FileReader>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<CloudForm>();
        containerBuilder.RegisterType<TagCloudPainter>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>()
            .SingleInstance();
        containerBuilder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<AppSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<TagSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<WordAnalyzer.WordAnalyzer>();
        containerBuilder.RegisterType<WordAnalyzerSettings>().AsSelf().SingleInstance();
        return containerBuilder.Build();
    }
}