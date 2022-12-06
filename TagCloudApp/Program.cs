using System.Reflection;
using Autofac;
using TagCloudApp.Domain;
using TagCloudCreator.Domain;
using TagCloudCreator.Domain.Providers;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure.Settings;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreatorExtensions;
using TagCloudCreatorExtensions.ImageSavers;
using TagCloudCreatorExtensions.WordsFileReaders;

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
        builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(TxtWordsFileReader))!)
            .AssignableTo<IWordsFileReader>()
            .As<IWordsFileReader>()
            .SingleInstance();

        builder.RegisterType<ImageSaverProvider>()
            .As<IImageSaverProvider>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(PngImageSaver))!)
            .AssignableTo<IImageSaver>()
            .As<IImageSaver>()
            .SingleInstance();

        builder.RegisterType<WordsInfoParser>()
            .As<IWordsInfoParser>()
            .SingleInstance();
        builder.RegisterType<WordsInfosProvider>()
            .As<IWordsInfosProvider>()
            .SingleInstance();


        builder.RegisterType<TagCloudPainter>()
            .SingleInstance();
        builder.RegisterType<WeightedTagCloudLayouterProvider>()
            .As<ITagCloudLayouterProvider>()
            .SingleInstance();
        builder.RegisterType<WeightedTagCloudLayouterSettings>()
            .SingleInstance();

        builder.RegisterType<TagCloudPaintSettings>()
            .SingleInstance();


        var container = builder.Build();
        return container;
    }
}