using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.FileReader;

namespace TagCloudTests.Infrastructure.FileReader;

internal class FileReaderFactoryTests
{
    private FileReaderFactory sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var docReader = new DocFileReader();
        var plainReader = new PlainTextFileReader();

        sut = new FileReaderFactory(new IFileReader[] { docReader, plainReader }, plainReader);
    }

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