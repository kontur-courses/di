using Autofac;
using TagCloudApp.Abstractions;
using TagCloudApp.Domain;
using TagCloudApp.Implementations;
using TagCloudApp.Infrastructure;

namespace TagCloudApp;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using var container = CreateContainer();

        ApplicationConfiguration.Initialize();
        Application.Run(container.Resolve<MainForm>());
    }

    private static IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<MainForm>()
            .SingleInstance();

        builder.RegisterType<SettingsManager>();
        builder.RegisterType<FileBlobStorage>()
            .As<IBlobStorage>();
        builder.RegisterType<XmlObjectSerializer>()
            .As<IObjectSerializer>();
        builder.Register(c => c.Resolve<SettingsManager>().Load())
            .As<AppSettings>()
            .As<IImagePathProvider>()
            .As<IWordsPathProvider>()
            .SingleInstance();
        builder.Register(c => c.Resolve<AppSettings>().ImageSettings)
            .SingleInstance();

        builder.RegisterType<PictureBoxImageHolder>()
            .As<PictureBoxImageHolder>()
            .As<IImageHolder>()
            .SingleInstance();

        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AssignableTo<IUiAction>()
            .As<IUiAction>()
            .SingleInstance();

        builder.RegisterType<WordsFileReaderProvider>()
            .As<IWordsFileReaderProvider>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AssignableTo<IWordsFileReader>()
            .As<IWordsFileReader>()
            .SingleInstance();
        
        builder.RegisterType<ImageSaverProvider>()
            .As<IImageSaverProvider>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AssignableTo<IImageSaver>()
            .As<IImageSaver>()
            .SingleInstance();
        
        builder.RegisterType<WordsSelector>()
            .As<IWordsSelector>()
            .SingleInstance();
        builder.RegisterType<WordsInfoProvider>()
            .As<IWordsInfoProvider>()
            .SingleInstance();


        builder.RegisterType<TagCloudPainter>()
            .SingleInstance();
        builder.RegisterType<WeightedTagCloudLayouterCreator>()
            .As<ITagCloudLayouterCreator>()
            .SingleInstance();

        builder.RegisterType<TagCloudLayouterSettings>()
            .SingleInstance();
        builder.RegisterType<TagCloudPaintSettings>()
            .SingleInstance();

       
        var container = builder.Build();
        return container;
    }
}