using Autofac;

namespace TagsCloudConsoleUI;

public static class Program
{
    public static void Main()
    {
        var containerBuilder = new ContainerBuilder();
        
        containerBuilder.RegisterType<ConsoleUi>()
            .As<IUiManager>()
            .SingleInstance();
        
        var containter = containerBuilder.Build();

        var ui = containter.Resolve<IUiManager>();
        
        ui.StartUi();
    }
}