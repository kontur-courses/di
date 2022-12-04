using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagsCloudContainerTests;

[TestFixture]
public class ImageDrawerTests
{
    [SetUp]
    public void SetUp()
    {
        builder = new ContainerBuilder();
        builder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>();
        builder.RegisterType<TestFileReader>().As<IEnumerable<string>>();
        builder.RegisterType<DefaultWordsHandler>().As<IWordsHandler>();
        builder.RegisterInstance(new SpiralCloudLayouter(new Point(0, 0))).As<ICloudLayouter>();
        builder.RegisterType<DefaultRectanglesDistributor>().As<IRectanglesDistributor>();
        builder.RegisterType<ImageSaver>().AsSelf();
    }

    private ContainerBuilder builder;
    private readonly string saveDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "outputs");
    private readonly string inputPath = @"default.txt";

    [Test]
    public void Default()
    {
        var savePath = Path.Combine(saveDir, "default.jpg");
        
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                Brush = new SolidBrush(Color.Aqua),
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                InputPath = inputPath,
                SavePath = savePath,
                BackgroundColor = Color.Black
            });
            builder.RegisterInstance(mock.Create<ISettingsProvider>()).As<ISettingsProvider>();
        }

        var container = builder.Build();
        container.Resolve<ImageSaver>().Save(container.Resolve<IImageDrawer>().DrawnBitmap);
    }

    [Test]
    public void ColorsTest()
    {
        var savePath = Path.Combine(saveDir, "colors.jpg");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                Brush = new SolidBrush(Color.Red),
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                InputPath = inputPath,
                SavePath = savePath,
                BackgroundColor = Color.Aquamarine
            });
            builder.RegisterInstance(mock.Create<ISettingsProvider>()).As<ISettingsProvider>();
        }

        var container = builder.Build();
        container.Resolve<ImageSaver>().Save(container.Resolve<IImageDrawer>().DrawnBitmap);
    }

    [Test]
    public void FontTest()
    {
        var savePath = Path.Combine(saveDir, "font.jpg");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                Brush = new SolidBrush(Color.Aqua),
                Font = new Font("Arial", 24, FontStyle.Italic),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                InputPath = inputPath,
                SavePath = savePath,
                BackgroundColor = Color.Black
            });
            builder.RegisterInstance(mock.Create<ISettingsProvider>()).As<ISettingsProvider>();
        }

        var container = builder.Build();
        container.Resolve<ImageSaver>().Save(container.Resolve<IImageDrawer>().DrawnBitmap);
    }

    [Test]
    public void DifferentLayoutTest()
    {
        var savePath = Path.Combine(saveDir, "layout.jpg");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                Brush = new SolidBrush(Color.Aqua),
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(6000, 6000),
                InputPath = inputPath,
                SavePath = savePath,
                BackgroundColor = Color.Black
            });
            builder.RegisterInstance(mock.Create<ISettingsProvider>()).As<ISettingsProvider>();
        }

        builder.RegisterInstance(new BlockCloudLayouter(new Point(0, 0))).As<ICloudLayouter>();

        var container = builder.Build();
        container.Resolve<ImageSaver>().Save(container.Resolve<IImageDrawer>().DrawnBitmap);
    }
}