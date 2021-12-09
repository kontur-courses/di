using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.FileReader;

namespace TagCloudTests.Infrastructure.FileReader;

internal class FileReaderFactoryTests
{
    private readonly FileReaderFactory sut = new();

    [TestCase("a.txt", typeof(PlainTextFileReader))]
    [TestCase("a.unknownformat", typeof(PlainTextFileReader))]
    [TestCase("a.doc", typeof(DocFileReader))]
    [TestCase("a.docx", typeof(DocFileReader))]
    public void Create_ShouldReturnCorrectReader(string inputPath, Type expectedType)
    {
        var actual = sut.Create(inputPath);

        actual.Should().BeOfType(expectedType);
    }
}