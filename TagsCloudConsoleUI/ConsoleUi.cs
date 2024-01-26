using System.Drawing.Imaging;
using Autofac;
using TagsCloudConsoleUI.Providers;
using TagsCloudContainer.BuildingOptions;
using TagsCloudContainer.Drawing;

namespace TagsCloudConsoleUI;

public class ConsoleUi : IUiManager
{
    private readonly ContainerBuilder _containerBuilder;
    
    public ConsoleUi(ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;
    }
    
    public void StartUi()
    {
        _containerBuilder.RegisterType<ConsoleSettingsProvider>()
            .As<ICommonOptionsProvider, IDrawingOptionsProvider>()
            .SingleInstance();
        
        var container = _containerBuilder.Build();

        BuildTagCloud(container.Resolve<IImageDrawer>(), ".", "image.png", ImageFormat.Png);
    }

    private static void BuildTagCloud(IImageDrawer imageDrawer, string dirPath, string fileName, ImageFormat imageFormat)
    {
        var bitmap = imageDrawer.DrawImage();
        DefaultImageDrawer.SaveImage(bitmap, dirPath, fileName, imageFormat);
        Console.WriteLine($"The image has been saved to \"{dirPath}\"");
    }
}