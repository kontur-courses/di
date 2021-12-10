using Autofac;
using Mono.Options;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class DefaultRunner : IRunner
{
    private readonly ILifetimeScope container;

    public DefaultRunner(ILifetimeScope container)
    {
        this.container = container;
    }

    public void Run(params string[] args)
    {
        ParseSettings(args);
        var vis = container.Resolve<IVisualizer>();
        var output = container.Resolve<OutputSettings>();
        var img = vis.GetBitmap();
        img.Save(output.OutputPath, output.ImageFormat.GetFormat());
    }

    private void ParseSettings(string[] args)
    {
        var allSettingsProviders = container.Resolve<IEnumerable<ICliSettingsProvider>>().ToList();
        var argsList = args.ToList();
        var allOptions = allSettingsProviders.Select(x => x.GetCliOptions()).ToList();
        var helperOptions = new OptionSet
        {
            { "h|?|help", "Show this help", (string v) => ShowHelp(allOptions) }
        };
        allOptions.Add(helperOptions);

        foreach (var item in allOptions)
        {
            argsList = item.Parse(argsList);
            if (!argsList.Any())
                break;
        }

        if (argsList.Any())
            throw new ArgumentException($"Unknown arguments encountered: [{string.Join(", ", argsList)}]");

        foreach (var provider in allSettingsProviders.OfType<IRequiredSettingsProvider>())
        {
            if (!provider.IsSet)
                throw new ArgumentException($"One of the required arguments were not provided: {provider.GetType().Name}");
        }
    }

    private static void ShowHelp(List<OptionSet> allOptions)
    {
        foreach (var option in allOptions)
        {
            option.WriteOptionDescriptions(Console.Out);
        }
    }
}
