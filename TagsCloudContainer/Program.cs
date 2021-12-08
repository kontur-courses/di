using Autofac;
using Mono.Options;
using System.Reflection;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;
public class Program
{
    public static void Main(string[] args)
    {
        //args = new[] { "-h", "--string", "tag1 Tag1 tag3 Tag2 tag1 TAG3 tag3 tag4 tag2 tag1", "--center", "500, 300", "--color", "red" };

        var assemblies = new List<Assembly>() { Assembly.GetExecutingAssembly() };
        var assemblyAdder = new OptionSet()
        {
            {"assemblies=",$"Specifies additional assemblies to use.",v => AddAssembliesFrom(assemblies,v.Split()) }
        };
        var leftAgrs = assemblyAdder.Parse(args);
        var builder = new ContainerBuilder();
        RegistrationHelper.RegisterServices(builder, assemblies.ToArray());
        var container = builder.Build();

        IRunner runner = container.Resolve<DefaultRunner>();
        var runnerSelector = new OptionSet()
        {
            {"runner=",$"Select runner to use. Defaults to {nameof(DefaultRunner)}",v => runner = container.ResolveKeyed<IRunner>(v) }
        };

        leftAgrs = runnerSelector.Parse(leftAgrs);

        runner.Run(leftAgrs.ToArray());
    }

    private static void AddAssembliesFrom(List<Assembly> assemblies, IEnumerable<string> assemblyNames)
    {
        foreach (var assembly in assemblyNames)
        {
            assemblies.Add(Assembly.LoadFrom(assembly));
        }
    }
}
