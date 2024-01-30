using FluentAssertions;
using NUnit.Framework;
using System.Reflection;
using System.Text.RegularExpressions;
using TagsCloud.Contracts;
using TagsCloud.Formatters;

namespace TagsCloud.Tests;

[TestFixture]
public partial class FileReadersTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        expectedLines = File.ReadAllLines($"{testDataPath}.txt")
                            .Select(line => line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                            .Select(array => array[0])
                            .Where(word => char.IsLetter(word[0]))
                            .ToArray();
    }

    private readonly string testDataPath = Path.Join("TestData", "test_data");
    private readonly IPostFormatter defaultFormatter = new DefaultPostFormatter();
    private readonly char[] separators = { ' ', ',', '.', ':', ';', '!', '?' };
    private string[] expectedLines;

    private readonly IFileReader[] fileReaders =
        Assembly
            .GetAssembly(typeof(IFileReader))!
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(inter => inter == typeof(IFileReader)))
            .Select(reader => (IFileReader)Activator.CreateInstance(reader)!)
            .ToArray();

    [Test]
    public void Readers_Should_ReadFileContentCorrectly()
    {
        foreach (var reader in fileReaders)
        {
            var actualLines = reader
                              .ReadContent($"{testDataPath}.{reader.SupportedExtension}", defaultFormatter)
                              .Where(line => !string.IsNullOrEmpty(line));

            actualLines.ShouldAllBeEquivalentTo(expectedLines);
        }
    }

    [Test]
    public void ReadersNames_Should_MatchSupportedExtensions()
    {
        foreach (var reader in fileReaders)
        {
            var match = ReaderNamePattern().Match(reader.GetType().Name);

            match.Success.Should().Be(true);
            match.Groups[1].Value.ToLower().ShouldBeEquivalentTo(reader.SupportedExtension);
        }
    }

    [GeneratedRegex("([A-Z]\\w*)FileReader")]
    private static partial Regex ReaderNamePattern();
}