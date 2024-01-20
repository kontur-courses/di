using System.Drawing;
using SixLabors.ImageSharp.Formats.Jpeg;
using TagsCloudContainer;

using var imageGenerator = new ImageGenerator(
    FileHandler.GetRelativeFilePath("out/res.jpg"),
    FileHandler.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
    30, 1920, 1080, new JpegEncoder { Quality = 100 });

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