using Autofac;
using TagCloud.UserInterface;

namespace TagCloud;

public class Program
{
    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        var settings = Configurator.Parse(args, builder);

        builder = Configurator.BuildWithSettings(settings, builder);

        var container = builder.Build();
        container.Resolve<IUserInterface>().Run(settings);
    }
}