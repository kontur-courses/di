using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.App.UI;
using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.FileReader;
using TagCloudContainer.Infrastructure.Filter;
using TagCloudContainer.Infrastructure.Layouter;
using TagCloudContainer.Infrastructure.Lemmatizer;
using TagCloudContainer.Infrastructure.Painter;
using TagCloudContainer.Infrastructure.Saver;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainerTests;

internal class ConsoleUITests
{
    private IAppSettings settings;
    private IUserInterface sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settings = new AppSettings { OutputPath = "outputTest" };
        var painter = new Painter(new RandomPalette(), new CircularCloudLayouter(settings), settings);
        sut = new ConsoleUI(new MockReader(), painter, new WordWeigher(), new ImageSaver(), new RussianLemmatizer(), new Filter());
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

    private class MockReader : IFileReader
    {
        public IEnumerable<string> GetLines(string inputPath)
        {
            yield return "test";
        }
    }
}