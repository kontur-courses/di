using System.Drawing.Imaging;
using Autofac;
using TagsCloudConsoleUI.Providers;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common;
using TagsCloudCore.Drawing;

namespace TagsCloudConsoleUI;

public static class Program
{
    public static void Main()
    {
        try
        {
            var containterBuilder = DiContainerBuilder.RegisterDefaultDependencies();
            
            containterBuilder.RegisterType<ConsoleSettingsProvider>()
                .As<ICommonOptionsProvider, IDrawingOptionsProvider>()
                .SingleInstance();

            var container = containterBuilder.Build();

            BuildTagCloud(container.Resolve<IImageDrawer>(), ".", "image.png", ImageFormat.Png);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private static void BuildTagCloud(IImageDrawer imageDrawer, string dirPath, string fileName, ImageFormat imageFormat)
    {
        var bitmap = imageDrawer.DrawImage();
        DefaultImageDrawer.SaveImage(bitmap, dirPath, fileName, imageFormat);
        Console.WriteLine($"The image has been saved to \"{dirPath}\"");
    }
}