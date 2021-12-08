using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.FileReader;

namespace TagCloudTests;

internal class PlainTextFileReaderTests
{
    private const string FileName = $"{nameof(PlainTextFileReader)}Test.txt";
    private readonly IFileReader sut = new PlainTextFileReader();

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        using var fs = File.Open(FileName, FileMode.Create);
        using var sw = new StreamWriter(fs);
        sw.Write($"firstline{Environment.NewLine}secondline{Environment.NewLine}thirdline{Environment.NewLine}{Environment.NewLine}");
    }

    [Test]
    public void GetLines_ShouldReturnCorrectLinesEnumerable()
    {
        var expected = new[] { "firstline", "secondline", "thirdline", "" };

        var actual = sut.GetLines(FileName);

        actual.Should().BeEquivalentTo(expected);
    }
}