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
        args = new[] { "-h", "--string", "tag1 Tag1 tag3 Tag2 tag1 TAG3 tag3 tag4 tag2 tag1", "--center", "500, 300", "--color", "red" };

        var builder = new ContainerBuilder();
        var assemblies = new[] { Assembly.GetExecutingAssembly() };
        RegistrationHelper.RegisterServices(builder, assemblies);
        var container = builder.Build();

        IRunner runner = container.Resolve<DefaultRunner>();
        var runnerSelector = new OptionSet()
        {
            {"runner",$"Select runner to use. Defaults to {nameof(DefaultRunner)}",v => runner = container.ResolveKeyed<IRunner>(v) }
        };

        var leftAgrs = runnerSelector.Parse(args);

        runner.Run(leftAgrs.ToArray());
    }
}
