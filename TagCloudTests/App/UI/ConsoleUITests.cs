using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.UI;
using TagCloud.App.UI.Console;
using TagCloud.App.UI.Console.Common;
using TagCloud.Infrastructure.FileReader;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Layouter;
using TagCloud.Infrastructure.Lemmatizer;
using TagCloud.Infrastructure.Painter;
using TagCloud.Infrastructure.Saver;
using TagCloud.Infrastructure.Weigher;

namespace TagCloudTests.App.UI;

internal class ConsoleUITests
{
    private IAppSettings settings;
    private IUserInterface sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settings = new AppSettings { OutputPath = "outputTest" };
        var layouter = new CircularCloudLayouter(settings);
        var factory = new CloudLayouterFactory(new ICloudLayouter[] { layouter }, layouter);
        var painter = new Painter(new RandomPalette(), factory, settings);
        sut = new ConsoleUI(new FakeFileReaderFactory(), painter, new WordWeigher(), new ImageSaver(), new RussianLemmatizer(), new Filter());
    }

    [Test]
    public void Run_TransferImageFileToSaver()
    {
        var filepath = $"{Environment.CurrentDirectory}\\{settings.OutputPath}.{settings.OutputFormat}";

        if (File.Exists(filepath))
            File.Delete(filepath);

        sut.Run(settings);

        File.Exists(filepath).Should().BeTrue();
    }

    private class FakeFileReaderFactory : IFileReaderFactory
    {
        public IFileReader Create(string filePath)
        {
            return new FakeReader();
        }
    }

    private class FakeReader : IFileReader
    {
        public IEnumerable<string> GetLines(string inputPath)
        {
            yield return "test";
        }

        public IReadOnlySet<string> GetSupportedExtensions()
        {
            throw new NotImplementedException(); //ignored
        }
    }
}