using System.Drawing;
using Color = SixLabors.ImageSharp.Color;
using TagsCloudContainer;

using var imageGenerator = new ImageGenerator(
    FileHandler.GetRelativeFilePath("out/res"), ImageEncodings.Png,
    FileHandler.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
    30, 1920, 1080,
    Color.FromRgb(33,0,46),
    frequency => (
        (byte)(frequency == 1 ? 84 : frequency <= 5 ? 255 : 57),
        (byte)(frequency == 1 ? 253 : frequency <= 5 ? 122 : 108),
        (byte)(frequency == 1 ? 158 : frequency <= 5 ? 254 : 255),
        (byte)Math.Min(255, 200 + frequency * 5)
    )
);

new TagCloudVisualizer(
    new CircularCloudLayouter(new Point(960, 540)),
    imageGenerator
).GenerateTagCloud(new WordsDataSet(FileHandler.ReadText("words")));

/*
using var imageGenerator = new ImageGenerator(
    FileHandler.GetRelativeFilePath("out/res"), Encoder.Jpg,
    FileHandler.GetSourceRelativeFilePath("JosefinSans-Regular.ttf"),
    30, 1920, 1080);

new TagCloudVisualizer(
    new CircularCloudLayouter(new Point(960, 540)),
    imageGenerator
).GenerateTagCloud(new WordsDataSet(Words.Preprocessing(FileHandler.ReadText("words"))));
*/