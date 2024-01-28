using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.Image;
using TagsCloudContainer.TagCloud;
using TagsCloudContainer.UI;
using TagsCloudContainer.utility;
using Color = SixLabors.ImageSharp.Color;

namespace TagsCloudContainer;

public static class Program
{
    /*
    -i="/Users/draginsky/Rider/di/TagsCloudContainer/src/words.txt"
    -o="/Users/draginsky/Rider/di/TagsCloudContainer/out/res"
    --fontpath="/Users/draginsky/Rider/di/TagsCloudContainer/src/JosefinSans-Regular.ttf"
     */
    public static void Main(string[] args)
    {
        using var uiContainer = UIContainerInit(args);

        var ui = uiContainer.GetService<IUI>()!;
        var obj = ui.Setup();
        if (obj == null) return;

        using var container = MainContainerInit(obj);

        container.GetService<TagCloudVisualizer>()!
            .GenerateTagCloud(container.GetService<WordHandler>()!
                .Preprocessing(container.GetService<WordDataSet>()!
                    .CreateFrequencyDict(container.GetService<ITextHandler>()!.ReadText()
                    )
                )
            );

        ui.View();
    }

    private static ServiceProvider UIContainerInit(string[] args)
    {
        ServiceCollection services = [];

        services.AddSingleton<IUI>(_ => new CLI(args));

        return services.BuildServiceProvider();
    }

    private static ServiceProvider MainContainerInit(ApplicationArguments args)
    {
        ServiceCollection services = [];

        services.AddTransient<ICircularCloudLayouter>(_ => new CircularCloudLayouter(
            new Point(args.Center[0], args.Center[1])
        ));

        var format = args.Format switch
        {
            "bmp" => ImageEncodings.Bmp,
            "gif" => ImageEncodings.Gif,
            "jpg" => ImageEncodings.Jpg,
            "png" => ImageEncodings.Png,
            "tiff" => ImageEncodings.Tiff,
            _ => ImageEncodings.Jpg
        };
        services.AddTransient<ImageGenerator>(_ =>
            new ImageGenerator(
                args.Output, format,
                args.FontPath,
                args.FontSize, args.Resolution[0], args.Resolution[1],
                Color.FromRgb(
                    (byte)args.Background[0],
                    (byte)args.Background[1],
                    (byte)args.Background[2]),
                (_, _) => (
                    (byte)args.Scheme[0],
                    (byte)args.Scheme[1],
                    (byte)args.Scheme[2],
                    (byte)args.Scheme[3])
            )
        );

        services.AddTransient<TagCloudVisualizer>();

        services.AddSingleton<ITextHandler>(_ => new FileTextHandler(args.Input));
        services.AddSingleton<WordHandler>(_ => new WordHandler(
            new FileTextHandler(args.Exclude),
            w => w.Length > 3)
        );
        services.AddTransient<WordDataSet>();

        return services.BuildServiceProvider();
    }
}

// new ImageGenerator(
//     Utility.GetRelativeFilePath("out/res"), format,
//     Utility.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
//     30, 1920, 1080,
//     Color.FromRgb(33, 0, 46),
//     (w, freq) => (
//         (byte)(freq == 1 ? 84 : freq <= 5 ? 255 : 57),
//         (byte)(freq == 1 ? 253 : freq <= 5 ? 122 : 108),
//         (byte)(freq == 1 ? 158 : freq <= 5 ? 254 : 255),
//         (byte)Math.Min(255, 55 + w.Length * 20)
//     )
// )