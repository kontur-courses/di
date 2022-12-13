using System.Collections.Concurrent;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class FileWordsLoaderTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        if (Directory.Exists(Dir))
            Directory.Delete(Dir, true);
        Directory.CreateDirectory(Dir);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (Directory.Exists(Dir))
            Directory.Delete(Dir, true);
    }

    [SetUp]
    public void SetUp()
    {
        var filepath = Path.Combine(Dir, $"{TestContext.CurrentContext.Test.Name}.txt");
        using (var file = File.Open(filepath, FileMode.OpenOrCreate))
        {
            file.Flush();
        }

        filePaths[TestContext.CurrentContext.Test.ID] = filepath;
    }

    private readonly ConcurrentDictionary<string, string> filePaths = new();
    private const string Dir = "TestFiles";

    [Test]
    public void Constructor_ThrowFileNotFoundException_OnNonExistentFile()
    {
        var filepath = filePaths[TestContext.CurrentContext.Test.ID];
        File.Delete(filepath);

        var act = () => new FileWordsLoader(filepath);

        act.Should().Throw<FileNotFoundException>()
            .WithMessage($"Could not find file '{Path.GetFullPath(filepath)}'.");
    }

    [Test]
    public void Constructor_ThrowFileNotFoundException_OnFileBeenDeletedAfterLoaderInit()
    {
        var filepath = filePaths[TestContext.CurrentContext.Test.ID];
        var loader = new FileWordsLoader(filepath);
        File.Delete(filepath);

        var act = () => loader.Load();

        act.Should().Throw<FileNotFoundException>()
            .WithMessage($"Could not find file '{Path.GetFullPath(filepath)}'.");
    }

    [Test]
    public void Load_ReturnEmptyEnumerable_OnEmptyFile()
    {
        var filepath = filePaths[TestContext.CurrentContext.Test.ID];
        var loader = new FileWordsLoader(filepath);

        var result = loader.Load();

        result.Should().BeEmpty();
    }

    [Test]
    public void Load_ReturnCorrectFileLines()
    {
        var lines = new[] { "Карл", "у", "Клары", "украл", "кораллы", "а", "Клара", "у", "Карла", "украла", "кларнет" };
        var filepath = filePaths[TestContext.CurrentContext.Test.ID];
        File.WriteAllLines(filepath, lines);
        var loader = new FileWordsLoader(filepath);

        var result = loader.Load();

        result.Should().Equal(lines);
    }

    [Test]
    public void Load_ReturnCorrectFileLines_AfterFileChanged()
    {
        var filepath = filePaths[TestContext.CurrentContext.Test.ID];
        var lines = new[] { "Карл", "у", "Клары", "украл", "кораллы", "а", "Клара", "у", "Карла", "украла", "кларнет" };
        File.WriteAllLines(filepath, lines);
        var loader = new FileWordsLoader(filepath);
        var resultBefore = loader.Load();
        var newLines = new[] { "От", "топота", "копыт", "пыль", "по", "полю", "летит" };
        File.WriteAllLines(filepath, newLines);

        var resultAfter = loader.Load();

        resultBefore.Should().Equal(lines);
        resultAfter.Should().Equal(newLines);
    }
}