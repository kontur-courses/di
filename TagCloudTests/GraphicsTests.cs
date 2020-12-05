using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using NUnit.Framework;
using TagCloud;
using TagCloud.App;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;

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
            builder.RegisterType<WordAnalyzer<string>>();

            builder.RegisterType<LowerCaseFilter>().As<IFilter<string>>();
            var myStemPath = Program.GetReleasePath("mystem");
            builder.RegisterType<WordTypeFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<WordCounterFilter>().As<IFilter<string>>();
            builder.RegisterType<WordThresholdFilter>().As<IFilter<string>>();
            builder.RegisterType<InterestingWordsFilter>().As<IFilter<string>>();
            builder.RegisterType<WordFontSizeFilter>().As<IFilter<string>>();
            builder.RegisterType<WordSizeFilter>().As<IFilter<string>>();

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
            var reader = container.Resolve<IReader<string>>();
            var wordAnalyzer = container.Resolve<WordAnalyzer<string>>();
            var painter = container.Resolve<IPainter<string>>();
            settingsFactory().Import(Program.GetDefaultSettings());
            
            var tokens = reader.ReadTokens();
            var analyzedTokens = wordAnalyzer.Analyze(tokens);
            image1 = painter.GetImage(analyzedTokens);
            tokens = reader.ReadTokens();
            analyzedTokens = wordAnalyzer.Analyze(tokens);
            image2 = painter.GetImage(analyzedTokens);
            
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

            var a = DateTime.Today;
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