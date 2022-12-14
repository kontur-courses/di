using System.Drawing;
using FluentAssertions;
using TagsCloudContainer;

namespace CloudGeneratorTests;

public class ArgsChecker_Should
{
    private ArgsChecker _argsChecker;

    [SetUp]
    public void SetUp()
    {
        _argsChecker = new ArgsChecker();
    }

    [Test]
    public void GetColors_WhenFileDoesntExist_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.Colors = "DoesntExist";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage("Файл с цветами не найден DoesntExist");
    }

    [Test]
    public void GetColors_WhenUnknownColor_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.Colors = TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/Colors/UnknownColor.txt");
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage("Неизвестный цвет! unk");
    }

    [Test]
    public void GetColors_WhenFilesExists_ReturnsColors()
    {
        var options = new CommandLineOptions();
        var path = TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/Colors/KnownColor.txt");
        options.Colors = path;
        var res = _argsChecker.Check(options);
        res.ColorsParsed.Should().BeEquivalentTo(new HashSet<Brush> { Brushes.Red });
    }

    [Test]
    public void GetColors_WhenDefaultFalue_ReturnsDefaultValue()
    {
        var options = new CommandLineOptions();
        var res = _argsChecker.Check(options);
        res.ColorsParsed.Should().BeEquivalentTo(new HashSet<Brush> { Brushes.Black });
    }

    [Test]
    public void GetFont_WhenFontIsNotAvailable_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.FontName = "UnkFont";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage($"Неизвестный шрифт {options.FontName}");
    }

    [Test]
    public void GetFont_WhenFontIsAvailable_ReturnsFont()
    {
        var options = new CommandLineOptions();
        options.FontName = "Arial";
        var res = _argsChecker.Check(options);
        res.FontName.Should().Be("Arial");
    }

    [Test]
    public void GetLayouter_WhenLayouterIsNotAvailable_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.LayouterName = "UnkLayoter";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage($"Неизвестная раскладка {options.LayouterName}");
    }

    [Test]
    public void GetOutputPath_WhenNull_ReturnsDefault()
    {
        var options = new CommandLineOptions();
        var res = _argsChecker.Check(options);
        res.OutputFile.Should().Be("Result.png");
    }

    [Test]
    public void GetOutputPath_WhenFileDoesntExist_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.OutputFile = "UnkPath";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage("Директория для сохранения файла не найдена! ");
    }

    [Test]
    public void GetOutputPath_WhenUnknownFormat_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.OutputFile = TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/Result.ppg");
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage("Неизвестный формат! ppg");
    }

    [Test]
    public void GetOutputPath_WhenPathIsRight_ReturnsPath()
    {
        var options = new CommandLineOptions();
        options.OutputFile = TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/Result.png");
        var res = _argsChecker.Check(options);
        res.OutputFile.Should().Be(options.OutputFile);
    }

    [Test]
    public void GetWordsToIgnore_WhenFileDoesntExist_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.WordsToIgnoreFile = "UnkPath";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage($"Файл со словами не найден! {options.WordsToIgnoreFile}");
    }

    [Test]
    public void GetWordsToIgnore_WhenFileIsNotEmpty_ReturnsSetOfIgnored()
    {
        var options = new CommandLineOptions();
        options.WordsToIgnoreFile =
            TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/WordsToIgnore/wordsToIgnore.txt");
        var res = _argsChecker.Check(options);
        res.WordsToIgnore.Should().BeEquivalentTo(new HashSet<string> { "ignoreme" });
    }

    [Test]
    public void GetSpPartsToIgnore_WhenFileDoesntExist_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.SpPartsToIgnoreFile = "UnkPath";
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage($"Файл с частями речи не найден! {options.SpPartsToIgnoreFile}");
    }

    [Test]
    public void GetSpPartsToIgnore_WhenUnknownSpeechPart_ThrowsException()
    {
        var options = new CommandLineOptions();
        options.SpPartsToIgnoreFile =
            TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/SpPartsToIgnore/Unknown.txt");
        var act = () => _argsChecker.Check(options);
        act.Should().Throw<Exception>().WithMessage("Неизвестная часть речи! ааа");
    }

    [Test]
    public void GetSpPartsToIgnore_WhenFileExists_ReturnsIgnored()
    {
        var options = new CommandLineOptions();
        options.SpPartsToIgnoreFile =
            TestsUtility.GetFullPathFromRelative("../../../TestCases/ArgsChecker/SpPartsToIgnore/spPartsToIgnore.txt");
        var res = _argsChecker.Check(options);
        res.SpPartsToIgnore.Should().BeEquivalentTo(new HashSet<string> { "гл" });
    }
}