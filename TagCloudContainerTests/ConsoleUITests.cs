﻿using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.UI;
using TagCloud.Infrastructure.Common;
using TagCloud.Infrastructure.FileReader;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Layouter;
using TagCloud.Infrastructure.Lemmatizer;
using TagCloud.Infrastructure.Painter;
using TagCloud.Infrastructure.Saver;
using TagCloud.Infrastructure.WordWeigher;

namespace TagCloudTests;

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