using Autofac;

namespace GuiApp;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var container = DiContainerBuilder.Build();
        ApplicationConfiguration.Initialize();
        Application.Run(container.Resolve<Form>());
    }
}