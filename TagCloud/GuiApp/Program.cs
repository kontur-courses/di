using Autofac;
using GuiApp.Controls;
using TagCloud;

namespace GuiApp;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        var container = DiContainerBuilder.Build();
        var form = container.Resolve<Form>();
        RenderButton.RenderRequired += (_, _) =>
        {
            var oldImage = Viewport.Instance.Image;
            Viewport.Instance.Image = container.Resolve<TagCloudConstructor>().Construct();
            oldImage?.Dispose();
        };
        
        Application.Run(form);
    }
}