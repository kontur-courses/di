using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.Image;
using TagsCloudContainer.TagCloud;
using TagsCloudContainer.utility;
using Color = SixLabors.ImageSharp.Color;

namespace TagsCloudContainer;

public static class Program
{
    public static void Main()
    {
        using var provider = ContainerInit();

        using var imageGenerator = new ImageGenerator(
            Utility.GetRelativeFilePath("out/res"), ImageEncodings.Jpg,
            Utility.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
            30, 1920, 1080,
            Color.FromRgb(33, 0, 46),
            (w, freq) => (
                (byte)(freq == 1 ? 84 : freq <= 5 ? 255 : 57),
                (byte)(freq == 1 ? 253 : freq <= 5 ? 122 : 108),
                (byte)(freq == 1 ? 158 : freq <= 5 ? 254 : 255),
                (byte)Math.Min(255, 55 + w.Length * 20)
            )
        );

        new TagCloudVisualizer(
            provider.GetService<ICircularCloudLayouter>()!,
            imageGenerator
        ).GenerateTagCloud(
            WordHandler.Preprocessing(
                WordDataSet.CreateFrequencyDict(
                    TextHandler.ReadText("words.txt")
                ),
                true,
                true,
                "boringCustom.txt",
                w => w.Length < 10
            )
        );
    }

    private static ServiceProvider ContainerInit()
    {
        ServiceCollection services = new();
        services.AddTransient<ICircularCloudLayouter>(_ => new CircularCloudLayouter(new Point(960, 540)));
        
        return services.BuildServiceProvider();
    }
}

// обязательные args:
// tagCloud  --in mars.docx  --out out/res  --format jpg  --font src/JosefinSans-Regular.ttf 30  --resolution 1920 1080

// все args:
// tagCloud  --in mars.docx  --out out/res  --format jpg  --font src/JosefinSans-Regular.ttf 30
// --resolution 1920 1080  --center 960 540  --bg 33 0 46  --scheme 84 255 57 255
// --excludeDefaultFile  --excludeDefaultRule  --exclude boringCustom.txt  

// shortcuts:
// tagCloud  -i mars.docx  -o out/res  -fmt jpg  -f src/JosefinSans-Regular.ttf 30  -r 1920 1080  -c 960 540
// -bg 33 0 46  -sch 84 255 57 255  -edf  -edr  -e boringCustom.txt  