using Autofac;
using CommandLine;
using TagCloud.AppSettings;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.FileSaver;
using TagCloud.Filter;
using TagCloud.Layouter;
using TagCloud.PointGenerator;
using TagCloud.UserInterface;
using TagCloud.WordRanker;
using TagCloud.WordsPreprocessor;

namespace TagCloudTests;

[TestFixture]
public class ConsoleUI_Should
{
    private IAppSettings settings;
    private IUserInterface sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settings = Parser.Default.ParseArguments<Settings>(new List<string>()).Value;

        var builder = new ContainerBuilder();
        builder.RegisterType<FakeReader>().As<IFileReader>();
        builder.RegisterType<ImageSaver>().As<ISaver>();
        builder.RegisterType<CloudDrawer>().As<IDrawer>();
        builder.RegisterType<WordRankerByFrequency>().As<IWordRanker>();
        builder.RegisterType<WordFilter>().As<IFilter>();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();

        builder.RegisterType<ConsoleUI>().As<IUserInterface>();

        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.Register(l =>
            new CircularLayouter(new SpiralGenerator(new Point(settings.CloudWidth / 2, settings.CloudWidth / 2),
                settings.CloudDensity))).As<ILayouter>();
        builder.Register(c => new FileReaderProvider(c.Resolve<IEnumerable<IFileReader>>())).As<IFileReaderProvider>();

        builder.Register(s => settings).AsImplementedInterfaces();

        var container = builder.Build();
        sut = container.Resolve<IUserInterface>();
    }

    [Test]
    public void GenerateFileWithCorrectSettings()
    {
        sut.Run(settings);

        File.Exists($"{settings.OutputPath}.{settings.ImageExtension}").Should().BeTrue();

        File.Delete($"{settings.OutputPath}.{settings.ImageExtension}");
    }

    private class FakeReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string inputPath)
        {
            yield return "test";
        }

        public IList<string> GetAviableExtensions()
        {
            return new List<string>() {"txt"};
        }
    }
}