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
        ServiceCollection services = [];
        services.AddTransient<ICircularCloudLayouter>(_ => new CircularCloudLayouter(new Point(960, 540)));
        services.AddTransient<TagCloudVisualizer>();
        services.AddTransient<ImageGenerator>(_ =>
            new ImageGenerator(
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
            )
        );
        services.AddTransient<ITextHandler>(_ => new FileTextHandler("words.txt"));
        services.AddTransient<WordHandler>(_ => new WordHandler(
            new FileTextHandler("boringWords.txt"),
            w => w.Length > 3)
        );
        services.AddTransient<WordDataSet>();

        var container = services.BuildServiceProvider();

        container.GetService<TagCloudVisualizer>()!
            .GenerateTagCloud(container.GetService<WordHandler>()!
                .Preprocessing(container.GetService<WordDataSet>()!.CreateFrequencyDict())
            );
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