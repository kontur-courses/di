using Autofac;
using TagCloud.ConsoleApp.CommandLine;
using TagCloud.ConsoleApp.CommandLine.Commands.Entities;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.ConsoleApp.CommandLine.Interfaces;

namespace TagCloud.ConsoleApp.DI;

public class Configurator
{
    public static ICommandService ConfigureApplication()
    {
        var builder = new ContainerBuilder();
        Domain.DI.Configurator.ConfigureDomain(builder);
        Utils.DI.Configure.ConfigureUtils(builder);
        ConfigureCommandLine(builder);

        var container = builder.Build();

        return container.Resolve<ICommandService>();
    }

    public static void ConfigureCommandLine(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(typeof(ICommand).Assembly)
            .As<ICommand>()
            .Except<HelpCommand>()
            .AsImplementedInterfaces()
            .SingleInstance();
        
        builder
            .RegisterType<HelpCommand>()
            .As<ICommand>()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .SingleInstance();

        builder
            .RegisterType<CommandService>()
            .As<ICommandService>()
            .SingleInstance();
    }
}