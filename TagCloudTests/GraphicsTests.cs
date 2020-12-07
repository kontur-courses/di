using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using NUnit.Framework;
using TagCloud;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Conveyors;

namespace TagCloudTests
{
    public class MockReader: IReader<string>
    {
        private readonly IEnumerable<string> words;

        public MockReader(IEnumerable<string> words)
        {
            this.words = words;
        }

        public IEnumerable<string> ReadTokens()
        {
            return words;
        }
    }
    [TestFixture]
    public class GraphicsTests
    {
        private ContainerBuilder builder;
        private Image image1;
        private Image image2;

        [SetUp]
        public void SetUp()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<TagCloudGenerator>().As<IImageGenerator>();
            builder.RegisterType<LowerCaseConveyor>().As<IConveyor<string>>();
            var myStemPath = Program.GetReleasePath("mystem");
            builder.RegisterType<WordTypeConveyor>()
                .As<IConveyor<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<WordCounterConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordThresholdConveyor>().As<IConveyor<string>>();
            builder.RegisterType<InterestingWordsConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordFontSizeConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordSizeConveyor>().As<IConveyor<string>>();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PlainEnvironment>().AsImplementedInterfaces();
            builder.RegisterType<SpiralStrategy>().As<ILayoutStrategy>();
            builder.RegisterType<TagCloudLayouter>().As<ILayouter<Size, Rectangle>>();

            builder.RegisterType<WordPainter>().As<IPainter<string>>();
            builder.RegisterType<Random>().SingleInstance();
            builder.RegisterType<ColorPicker>();

            builder.RegisterType<FileSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<ImagePathSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<ImageSizeSettingsManager>().AsImplementedInterfaces();
        }


        [Test]
        public void GenerationOnSameSettings_ImagesAreEqual()
        {
            var words = new[]
            {
                "компьютер", "компьютер", "компьютер",
                "компьютер", "компьютер", "компьютер", 
                "компьютер", "компьютер", "компьютер", 
                "компьютер", "компьютер", "компьютер",
                
                "компьютер", "компьютер", "компьютер",
                "компьютер", "компьютер", "компьютер", 
                "компьютер", "компьютер", "компьютер", 
                "компьютер", "компьютер", "компьютер",
                "слон", "слон", "слон", "слон", "слон",
                "слон", "слон", "слон", "слон", "слон",
                "слон", "слон", "слон", "слон", "слон",
                "слон", "слон", "слон", "слон", "слон",
                "слон", "слон", "слон", "слон", "слон",
            };
            builder.RegisterType<MockReader>().WithParameter("words", words).As<IReader<string>>();
            var container = builder.Build();
            var settingsFactory = container.Resolve<Func<Settings>>();
            var generator = container.Resolve<IImageGenerator>();
            settingsFactory().Import(Program.GetDefaultSettings());
            
            image1 = generator.Generate();
            image2 = generator.Generate();
            
            ImageAssert.AreEqual((Bitmap) image1, (Bitmap) image2);
        }

        [TearDown]
        public void TearDown()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var failedDir = "Failed";
            path = Path.Combine(path, failedDir);
            Directory.CreateDirectory(path);
            var dayTestsDir = $"{DateTime.Now:(yyyy-MM-dd)}";
            path = Path.Combine(path, dayTestsDir);
            Directory.CreateDirectory(path);
            var minuteTestsDir = $"{DateTime.Now:hh;mm}";
            path = Path.Combine(path, minuteTestsDir);
            Directory.CreateDirectory(path);
            path = Path.Combine(path, TestContext.CurrentContext.Test.Name);
            Directory.CreateDirectory(path);
            
            image1.Save(Path.Combine(path, GetName(image1)));
            image2.Save(Path.Combine(path, GetName(image1)));
            
            using (var outputFile = new StreamWriter(Path.Combine(path, "StackTrace.txt")))
            {
                outputFile.WriteLine(TestContext.CurrentContext.Result.Message);
                outputFile.WriteLine(TestContext.CurrentContext.Result.StackTrace);
            }
            
            image1.Dispose();
            image2.Dispose();
        }

        private string GetName(Image image)
        {
            return $"({image.Width}x{image.Height})[{Guid.NewGuid()}].bmp";
        }
    }
}