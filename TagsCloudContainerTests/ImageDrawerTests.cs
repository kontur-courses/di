using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Colorers;
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
        builder.RegisterType<DefaultRectanglesDistributor>().As<IRectanglesDistributor>();
        builder.RegisterType<DefaultWordsHandler>().As<IWordsHandler>();
        builder.RegisterType<SimpleColorProvider>().As<IColorProvider>();
        builder.RegisterInstance(new SpiralCloudLayouter(Point.Empty)).As<ICloudLayouter>();
    }

    private ContainerBuilder builder;
    private readonly string saveDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "outputs");
    private readonly string inputPath = @"default.txt";

    [Test]
    public void Default()
    {
        var savePath = Path.Combine(saveDir, "default.png");

        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            builder.RegisterInstance(settings).As<ISettingsProvider>();

        }

        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }

    [Test]
    public void ColorsTest()
    {
        var savePath = Path.Combine(saveDir, "colors.png");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Red,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Aquamarine
            });
            var settings = mock.Create<ISettingsProvider>();
            
            builder.RegisterInstance(settings).As<ISettingsProvider>();
        }
        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }

    [Test]
    public void FontTest()
    {
        var savePath = Path.Combine(saveDir, "font.png");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font("Arial", 24, FontStyle.Italic),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            
            builder.RegisterInstance(settings).As<ISettingsProvider>();
        }
        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();

        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }

    [Test]
    public void DifferentLayoutTest()
    {
        var savePath = Path.Combine(saveDir, "layout.png");
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(6000, 6000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            
            builder.RegisterInstance(settings).As<ISettingsProvider>();
        }
        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        builder.RegisterInstance(new BlockCloudLayouter(Point.Empty)).As<ICloudLayouter>();
        
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }

    [Test]
    public void FilterTest()
    {
        var savePath = Path.Combine(saveDir, "filter.png");

        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.6f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            builder.RegisterInstance(settings).As<ISettingsProvider>();
        }
        var testFileReader = new TestFileReader(inputPath, @"filter.txt");
        builder.RegisterInstance(testFileReader).As<IWordFilterProvider>();
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        builder.RegisterType<WordsHandlerWithFilter>().As<IWordsHandler>();
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }
    
    [Test]
    public void RandomColorsTest()
    {
        var savePath = Path.Combine(saveDir, "random.png");

        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            builder.RegisterInstance(settings).As<ISettingsProvider>();

        }

        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        builder.RegisterType<RandomColorProvider>().As<IColorProvider>();
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }
    
    [Test]
    public void TransparencyColorsTest()
    {
        var savePath = Path.Combine(saveDir, "transparent.png");

        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISettingsProvider>().Setup(s => s.Settings).Returns(new Settings
            {
                FontColor = Color.Aqua,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                FrequencyRatio = 1.3f,
                ImageSize = new Size(5000, 5000),
                BackgroundColor = Color.Black
            });
            var settings = mock.Create<ISettingsProvider>();
            builder.RegisterInstance(settings).As<ISettingsProvider>();

        }

        var testFileReader = new TestFileReader(inputPath, null);
        builder.RegisterInstance(testFileReader).As<IWordSequenceProvider>();
        builder.RegisterType<TransparencyOverFrequencyColorProvider>().As<IColorProvider>();
        var container = builder.Build();
        ImageSaver.Save(container.Resolve<IImageDrawer>().DrawImage(), savePath);
    }
}