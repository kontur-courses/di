using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Autofac;
using McMaster.Extensions.CommandLineUtils;
using TagsCloudConsoleUI.Providers;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Drawing.Colorers;
using TagsCloudContainer.DrawingOptions;
using TagsCloudContainer.TagCloudForming;
using TagsCloudContainer.Utils;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordGrouping;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudConsoleUI;

public class ConsoleUi : IUiManager
{
    public void StartUi()
    {
        var containerBuilder = new ContainerBuilder();
        RegisterWordProvider(GetPathToWordsFile(), containerBuilder);

        RegisterCloudBuildingOptions(containerBuilder);
        
        RegisterAlgorithm(CloudAlgorithmProviders.RegisteredProviders, "Choose the cloud forming algorithm:",
            containerBuilder);

        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();
        containerBuilder.RegisterInstance(new DefaultWordFilter(new TxtFileWordParser("../../../../TagsCloudContainer/Resources/filter.txt"))).As<IWordFilter>()
            .SingleInstance();
        containerBuilder.RegisterType<DefaultWordProcessor>().As<IProcessedWordProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultWordCloudDistributor>().As<IWordCloudDistributorProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();
        
        var container = containerBuilder.Build();

        BuildTagCloud(container.Resolve<IImageDrawer>(), ".", "image.png", ImageFormat.Png);
    }

    private void RegisterCloudBuildingOptions(ContainerBuilder containerBuilder)
    {
        var font = GetFont();
        var backgroundColor = GetRgbColor("Enter background color in RGB format separated by space");
        
        RegisterColoringAlgorithm(out var fontColor, containerBuilder);
        
        var imageSide =
            GetInteger(
                "Enter the image's desired size in px. The image will be a square. It must range from 500 px to 5000 px.",
                500, 5000);
        var imageSize = new Size(imageSide, imageSide);
        var frequencyScaling =
            GetInteger(
                "Enter the frequency scaling (a positive integer). It Determines the scale to word frequency ratio.", 2,
                100);
        
        containerBuilder
            .RegisterInstance(new DefaultOptionsProvider(new Options(fontColor, backgroundColor, imageSize, font,
                frequencyScaling)))
            .As<IOptionsProvider>().SingleInstance();
    }

    private static void BuildTagCloud(IImageDrawer imageDrawer, string dirPath, string fileName, ImageFormat imageFormat)
    {
        var bitmap = imageDrawer.DrawImage();
        DefaultImageDrawer.SaveImage(bitmap, dirPath, fileName, imageFormat);
        Console.WriteLine($"The image has been saved to \"{dirPath}\"");
    }

    private void RegisterAlgorithm(IReadOnlyDictionary<string, Action<ContainerBuilder>> registeredAlgorithms,
        string prompt, ContainerBuilder containerBuilder)
    {
        var sb = new StringBuilder($"{prompt}\n");
        foreach (var algorithmProvider in registeredAlgorithms.Keys)
            sb.AppendLine(algorithmProvider);

        while (true)
        {
            var algorithm = Prompt.GetString(sb.ToString(), "", promptColor: ConsoleColor.DarkGreen);
            if (registeredAlgorithms.TryGetValue(algorithm!, out var provider))
            {
                provider(containerBuilder);
                break;
            }

            Console.WriteLine("Specified algorithm isn't supported. Try again.");
        }
    }

    private static int GetInteger(string prompt, int lowerAllowedBoundary, int upperAllowedBoundary)
    {
        while (true)
        {
            var intString = Prompt.GetString(prompt, "", ConsoleColor.DarkGreen);
            if (int.TryParse(intString, out var parsed) && parsed >= lowerAllowedBoundary &&
                parsed <= upperAllowedBoundary)
                return parsed;
            Console.WriteLine("Given number is invalid. Try again.");
        }
    }

    private void RegisterColoringAlgorithm(out Color fontColor, ContainerBuilder containerBuilder)
    {
        if (Prompt.GetYesNo("Do you want to use a custom coloring algorithm?", false, ConsoleColor.DarkGreen))
        {
            RegisterAlgorithm(ColorerProviders.RegisteredProviders, "Choose the algorithm:", containerBuilder);

            fontColor = Color.White;
            return;
        }

        containerBuilder.RegisterType<DefaultWordColorer>().As<IWordColorer>().SingleInstance();
        fontColor = GetRgbColor("Enter font color in RGB format separated by space");
    }

    private static Color GetRgbColor(string prompt)
    {
        while (true)
        {
            var colorString = Prompt.GetString(prompt,
                promptColor: ConsoleColor.DarkGreen);
            if (DrawingUtils.TryParseRgb(colorString, out var color))
                return color;
            Console.WriteLine("Ivalid color format. Try again.");
        }
    }

    private static Font GetFont()
    {
        var fontName = Prompt.GetString("Enter font name", promptColor: ConsoleColor.DarkGreen) ??
                       "Microsoft Sans Serif";
        while (true)
        {
            var fontSizeStr = Prompt.GetString("Enter font size in pt", promptColor: ConsoleColor.DarkGreen);
            if (float.TryParse(fontSizeStr, out var fontSize) && fontSize > 0)
                return new Font(fontName, fontSize);
            Console.WriteLine("Font size must be a correct positive number. Try again.");
        }
    }

    private void RegisterWordProvider(string path, ContainerBuilder containerBuilder)
    {
        var ext = Path.GetExtension(path);
        if (WordProviders.RegisteredProviders.TryGetValue(ext, out var provider))
        {
            provider(containerBuilder, path);
            return;
        }

        Console.WriteLine("This extension is not supported. Try another one.");
        StartUi();
    }

    private static string GetPathToWordsFile()
    {
        while (true)
        {
            var path = Prompt.GetString("Enter the path to the file with words", promptColor: ConsoleColor.DarkGreen);
            if (File.Exists(path))
                return path;
            Console.WriteLine("Provided path is invalid. Try again.");
        }
    }
}