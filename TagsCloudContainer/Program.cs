using System.Drawing;
using Color = SixLabors.ImageSharp.Color;
using TagsCloudContainer.Image;
using TagsCloudContainer.TagCloud;
using TagsCloudContainer.utility;

using var imageGenerator = new ImageGenerator(
    Utility.GetRelativeFilePath("out/res"), ImageEncodings.Jpg,
    Utility.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
    30, 1920, 1080,
    Color.FromRgb(33, 0, 46),
    (w, freq) => (
        (byte)(freq == 1 ? 84 : freq <= 5 ? 255 : 57),
        (byte)(freq == 1 ? 253 : freq <= 5 ? 122 : 108),
        (byte)(freq == 1 ? 158 : freq <= 5 ? 254 : 255),
        (byte)Math.Min(255, 255 - w.Length * 20)
    )
);

new TagCloudVisualizer(
    new CircularCloudLayouter(new Point(960, 540)),
    imageGenerator
).GenerateTagCloud(
    WordHandler.Preprocessing(
        WordDataSet.CreateFrequencyDict(
            TextHandler.ReadText("mars.docx")
        ), 
        true, 
        "boringCustom.txt", 
        w => w.Length < 10
    )
);