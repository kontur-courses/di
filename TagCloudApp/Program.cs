using System.Reflection;
using Autofac;
using MyStemWrapper;
using TagCloudApp.Domain;
using TagCloudCreator.Domain;
using TagCloudCreator.Domain.Providers;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure;
using TagCloudCreator.Infrastructure.Settings;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreatorExtensions;
using TagCloudCreatorExtensions.ImageSavers;
using TagCloudCreatorExtensions.WordsFileReaders;
using TagCloudCreatorExtensions.WordsFilters;
using TagCloudCreatorExtensions.WordsFilters.Settings;

namespace TagCloudApp;

internal static class Program
{
    private const string MyStemExecutablePath = "../../../../MyStemBin/mystem.exe";

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

        builder.RegisterType<AppSettingsProvider>()
            .AsSelf()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<PictureBoxImageHolder>()
            .AsSelf()
            .As<IImageHolder>()
            .SingleInstance();

        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AssignableTo<IUiAction>()
            .As<IUiAction>()
            .SingleInstance();

        builder.RegisterType<ImageSaverProvider>()
            .As<IImageSaverProvider>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(PngImageSaver))!)
            .AssignableTo<IImageSaver>()
            .As<IImageSaver>()
            .SingleInstance();

        builder.RegisterType<WordsFileReaderProvider>()
            .As<IWordsFileReaderProvider>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(TxtFileReader))!)
            .AssignableTo<IFileReader>()
            .As<IFileReader>()
            .SingleInstance();

        builder.Register(_ => new MyStemWordsGrammarInfoParser(MyStemExecutablePath))
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<MyStemWordsNormalizer>()
            .As<IWordsNormalizer>()
            .SingleInstance();
        builder.RegisterType<FiltersSettings>()
            .AsSelf()
            .AsImplementedInterfaces()
            .SingleInstance();
        builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(WordsLengthFilter))!)
            .AssignableTo<IWordsFilter>()
            .As<IWordsFilter>()
            .SingleInstance();
        builder.RegisterType<WordsInfoParser>()
            .As<IWordsInfoParser>()
            .SingleInstance();

        builder.RegisterType<WordsPaintDataProvider>()
            .As<IWordsPaintDataProvider>();

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