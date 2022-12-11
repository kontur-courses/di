using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextProviders;

namespace TagsCloudVisualization.Tests.TextProviderTests;

[TestFixture]
public class TxtTextProviderTests
{
    private readonly string filepath = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");

    private TxtTextProvider textProvider;

    [OneTimeSetUp]
    public void CreateFile()
    {
        File.Create(filepath).Close();
    }

    [OneTimeTearDown]
    public void DeleteFile()
    {
        File.Delete(filepath);
    }

    [SetUp]
    public void SetUp()
    {
        textProvider = new TxtTextProvider(filepath);
    }

    [TearDown]
    public void TearDown()
    {
        File.WriteAllText(filepath, String.Empty);
    }

    [TestCase(new object[] { "first" })]
    [TestCase(new object[] { "first", "second" })]
    public void GetWords_ShouldReturnWords(params string[] input)
    {
        File.WriteAllLines(filepath, input);

        var words = textProvider.GetText();

        words.Should().Equal(input);
    }
}