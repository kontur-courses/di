using Autofac;
using Mono.Options;
using System.Reflection;
using TagsCloudContainer.Defaults;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer;
public class Program
{
    public static void Main(string[] args)
    {
        args = new[] { "-h", "--string", "tag1 Tag1 tag3 Tag2 tag1 TAG3 tag3 tag4 tag2 tag1", "--center", "100, 200", "--color", "red" };
        var builder = new ContainerBuilder();
        var assemblies = new[] { Assembly.GetExecutingAssembly() };
        RegisterServices(builder, assemblies);
        RegisterSettingsProviders(builder, assemblies);
        var container = builder.Build();
        ParseSettings(args, container);

        Run(container);
    }

    private static void ParseSettings(string[] args, IContainer container)
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

        foreach(var provider in allSettingsProviders.OfType<IRequiredSettingsProvider>())
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

    private static void Run(IContainer container)
    {
        var vis = container.Resolve<IVisualizer>();
        var output = container.Resolve<OutputSettings>();
        var img = vis.GetBitmap();
        img.Save(output.OutputPath, output.ImageFormat.GetFormat());
    }

    private static void RegisterServices(ContainerBuilder builder, Assembly[] assemblies)
    {
        var methods = assemblies.SelectMany(
            assembly => assembly.GetTypes()
            .SelectMany(
                type => type.GetMethods()
                .Where(
                    method => method.GetCustomAttribute<RegisterAttribute>() != null && method.IsStatic
                    )
                 )
             );

        foreach (var method in methods)
        {
            method.Invoke(null, new[] { builder });
        }
    }

    private static void RegisterSettingsProviders(ContainerBuilder builder, Assembly[] assemblies)
    {
        builder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ICliSettingsProvider>()
            .AsSelf().As<ICliSettingsProvider>()
            .SingleInstance();
    }
}
