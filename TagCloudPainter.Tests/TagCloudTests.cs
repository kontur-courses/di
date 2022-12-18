using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Builders;
using TagCloudPainter.Coloring;
using TagCloudPainter.Common;
using TagCloudPainter.FileReader;
using TagCloudPainter.Layouters;
using TagCloudPainter.Lemmaizers;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;
using TagCloudPainter.Savers;
using TagCloudPainter.Sizers;

namespace TagCloudPainter.Tests;

public class TagCloudTests
{
    public TagCloudSaver Saver { get; set; }

    [SetUp]
    public void SetUp()
    {
        var settings = new ImageSettings
        {
            BackgroundColor = Color.Black,
            Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point),
            Size = new Size(500, 500)
        };
        var imageProvider = new ImageSettingsProvider { ImageSettings = settings };
        var parserProvider = new ParseSettingsProvider { ParseSettings = new ParseSettings() };
        var reader = new TxtReader();
        var lemmaizer = new Lemmaizer(myStamPath());
        var wordPreprocessor = new WordPreprocessor(parserProvider, lemmaizer);
        var wordSizer = new WordSizer(imageProvider);
        var layouter = new CircularCloudLayouter(new Point(250, 250), Math.PI / 12, 0.25);
        var wordColoring = new WhiteWordColoring();
        var builder = new CloudElementBuilder(wordSizer, layouter);
        var painter = new CloudPainter(imageProvider, wordColoring);
        Saver = new TagCloudSaver(reader, wordPreprocessor, builder, painter);
    }

    [Test]
    public void TagCloud_Should_CreateImage()
    {
        var input = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Test.txt");
        var output = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Output.png");
        Saver.SaveTagCloud(input, output, ImageFormat.Png);

        File.Exists(output).Should().BeTrue();
    }

    [Test]
    public void TagCloud_Shouild_Fail_If_Input_Not_Exist()
    {
        var input = "text.txt";
        var output = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Output.png");
        var save = () => Saver.SaveTagCloud(input, output, ImageFormat.Png);

        save.Should().Throw<FileNotFoundException>();
    }


    private string myStamPath()
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "Lemmaizers", "mystem.exe");
    }
}