using Autofac;
using CommandLine;
using TagCloud.AppSettings;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.Filter;
using TagCloud.UserInterface;

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
        builder = Configurator.BuildWithSettings(settings, builder);
        builder.RegisterType<FakeReader>().As<IFileReader>();
        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.RegisterType<WordFilter>().As<IFilter>();

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

        public IList<string> GetAvailableExtensions()
        {
            return new List<string>() { "txt" };
        }
    }
}