using Autofac;
using CircularCloudLayouter;
using CircularCloudLayouter.WeightedLayouter;
using TagCloudApp.Abstractions;
using TagCloudApp.Domain;
using TagCloudApp.Implementations;

namespace TagCloudApp;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<MainForm>()
            .SingleInstance();
        builder.RegisterType<PictureBoxImageHolder>()
            .As<PictureBoxImageHolder>()
            .As<IImageHolder>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AssignableTo<IUiAction>()
            .As<IUiAction>()
            .SingleInstance();
        builder.RegisterType<AppSettings>()
            .As<IImageDirectoryProvider>()
            .SingleInstance();
        builder.Register(c => c.Resolve<AppSettings>().ImageSettings)
            .SingleInstance();
        builder.RegisterType<TagCloudPainter>();
        builder.RegisterType<WeightedTagCloudLayouter>()
            .As<ITagCloudLayouter>();
        builder.RegisterType<TagCloudLayouterSettings>()
            .SingleInstance();
        builder.RegisterType<TagCloudDrawSettings>()
            .SingleInstance();
        builder.RegisterType<WordsInfoProvider>()
            .As<IWordsInfoProvider>();
        builder.RegisterType<WordsSelector>()
            .As<IWordsSelector>();
        builder.RegisterType<TxtWordsLoader>()
            .As<IWordsLoader>();
        builder.RegisterType<WordsFilePathProvider>()
            .SingleInstance();
        var container = builder.Build();

        ApplicationConfiguration.Initialize();
        Application.Run(container.Resolve<MainForm>());
    }
}