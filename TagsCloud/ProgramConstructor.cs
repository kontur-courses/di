using System.Drawing;
using System.Reflection;
using Autofac;
using TagsCloud.App;
using TagsCloud.App.Settings;
using TagsCloud.CloudLayouter;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

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
        containerBuilder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>()
            .SingleInstance();
        containerBuilder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<AppSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<TagSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<WordAnalyzer.WordAnalyzer>();
        containerBuilder.RegisterType<TagCloudPainter>();
        containerBuilder.RegisterType<WordAnalyzerSettings>().AsSelf().SingleInstance();
        containerBuilder.Register<FlowerSpiral>(c =>
        {
            var appSettings = c.Resolve<ImageSettings>();
            return new FlowerSpiral(new Point(appSettings.Width / 2, appSettings.Height / 2));
        }).AsSelf();

        containerBuilder.Register<Spiral>(c =>
        {
            var imageSettings = c.Resolve<ImageSettings>();
            return new Spiral(new Point(imageSettings.Width / 2, imageSettings.Height / 2));
        }).AsSelf();
        return containerBuilder.Build();
    }
}