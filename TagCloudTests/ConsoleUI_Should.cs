using Autofac;
using CommandLine;
using TagCloud.CloudDrawer;
using TagCloud.CloudSaver;
using TagCloud.FileReader;
using TagCloud.Layouter;
using TagCloud.PointGenerator;
using TagCloud.Settings;
using TagCloud.UserInterface;
using TagCloud.WordFilter;
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
        builder.RegisterType<CloudSaver>().As<ICloudSaver>();
        builder.RegisterType<CloudDrawer>().As<IDrawer>();
        builder.RegisterType<WordRankerByFrequency>().As<IWordRanker>();
        builder.RegisterType<WordFilter>().As<IFilter>();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();

        builder.RegisterType<ConsoleUI>().As<IUserInterface>();

        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.Register(l =>
            new Layouter(new SpiralGenerator(new Point(settings.CloudWidth / 2, settings.CloudWidth / 2),
                settings.CloudDensity))).As<ILayouter>();

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
    }
}