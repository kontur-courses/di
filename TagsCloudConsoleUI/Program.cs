using System.Drawing.Imaging;
using Autofac;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common;
using TagsCloudCore.Common.Enums;
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

            BuildTagCloud(container.Resolve<IImageDrawer>(),
                container.Resolve<ICommonOptionsProvider>().CommonOptions.WordColorer, ".", "image.png",
                ImageFormat.Png);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void BuildTagCloud(IImageDrawer imageDrawer, WordColorerAlgorithm colorerAlgorithm, string dirPath,
        string fileName, ImageFormat imageFormat)
    {
        var bitmap = imageDrawer.DrawImage(colorerAlgorithm);
        DefaultImageDrawer.SaveImage(bitmap, dirPath, fileName, imageFormat);
        Console.WriteLine($"The image has been saved to \"{dirPath}\"");
    }
}