using DryIoc;
using McMaster.Extensions.CommandLineUtils;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Cli;

[Command(Name = "draw")]
[HelpOption("-?")]
public class Program
{
    [Option(CommandOptionType.SingleOrNoValue)]
    private string JsonSettingsFilename { get; } =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tag-cloud-settings.json");

    [Option(CommandOptionType.SingleOrNoValue)]
    private string WordsFileName { get; } =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tag-cloud-words.txt");

    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    /// <summary>
    ///     Using api of McMaster.Extensions.CommandLineUtils
    /// </summary>
    /// <param name="app"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>status code of program execution: 1 if settings isn't provided, in case of success execution returns 0</returns>
    private int OnExecute(CommandLineApplication app, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(JsonSettingsFilename)
            || string.IsNullOrWhiteSpace(WordsFileName))
        {
            app.ShowHelp();
            return 1;
        }

        var (selector, drawer) = GetServices();

        var words = File.ReadAllLines(WordsFileName);
        var cloudWords = selector.RecognizeFunnyCloudWords(words);
        drawer.Draw(cloudWords);
        return 0;
    }

    private (IFunnyWordsSelector selector, MultiDrawer drawer) GetServices()
    {
        var container = ContainerHelper.RegisterDefaultSingletonContainer();
        container.Register<IGraphicsProvider, CliGraphicsProvider>(Reuse.Singleton);
        container.RegisterDelegate(r =>
            (CliGraphicsProviderSettings)r.Resolve<Settings>().GraphicsProviderSettings);
        var jsonSettingsFactory = new JsonSettingsFactory(JsonSettingsFilename);
        container.RegisterDelegate<ISettingsFactory>(r => jsonSettingsFactory,
            Reuse.Singleton);

        var selector = container.Resolve<IFunnyWordsSelector>();
        var drawer = container.Resolve<MultiDrawer>();
        return (selector, drawer);
    }
}