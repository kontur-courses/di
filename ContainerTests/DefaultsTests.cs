using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults;
using TagsCloudContainer.Defaults.MyStem;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace ContainerTests;

public class DefaultsTests
{
    private static TSetting ParseSettings<TSetting>(TSetting settingObj, params string[] args)
        where TSetting : ICliSettingsProvider
    {
        settingObj.GetCliOptions().Parse(args);
        return settingObj;
    }

    [Test]
    public void StringReader_Should_CorrectlyReadText()
    {
        var text = $"abcd{Environment.NewLine}efg{Environment.NewLine}hkl";
        var expectedResult = text.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
        var inputSettings = ParseSettings(new InputSettings(), "--string", text);
        var reader = new SimpleStringReader(inputSettings);

        var actualResult = reader.ReadLines().ToArray();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void FileReader_Should_CorrectlyReadText()
    {
        var text = $"abcd{Environment.NewLine}efg{Environment.NewLine}hkl";
        var path = "test.txt";
        File.WriteAllText(path, text);

        var expectedResult = text.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
        var inputSettings = ParseSettings(new InputSettings(), "--files", path);
        var reader = new FileReader(inputSettings);

        var actualResult = reader.ReadLines().ToArray();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void TextAnalyzer_Should_CorrectlyCollectTextStats()
    {
        var text = new[] { "abcd", "efg", "hkl" };
        var fakeStats = A.Fake<ITextStats>();
        A.CallTo(() => fakeStats.Statistics).Returns(text.ToDictionary(x => x, x => 1));
        A.CallTo(() => fakeStats.TotalWordCount).Returns(text.Length);

        var fakeReader = A.Fake<ITextReader>();
        A.CallTo(() => fakeReader.ReadLines()).Returns(text);

        var textAnalyzerSettings = new TextAnalyzerSettings();
        var textAnalyzer = new TextAnalyzer(new[] { fakeReader }, Array.Empty<IWordNormalizer>(), Array.Empty<IWordFilter>(), textAnalyzerSettings);

        var actualResult = textAnalyzer.AnalyzeText();

        actualResult.Statistics.Should().BeEquivalentTo(fakeStats.Statistics);
        actualResult.TotalWordCount.Should().Be(fakeStats.TotalWordCount);
    }

    [Test]
    public void StemNormilizer_Should_CorrectlyTakeStemFromWord()
    {
        var text = new[] { "кот", "коты", "котов" };
        var expectedReuslt = new[] { "кот", "кот", "кот" };

        var myStem = new MyStem();

        var stemNormalizer = new StemNormalizer(myStem);
        var actualResult = text.Select(stemNormalizer.Normalize).ToArray();

        actualResult.Should().BeEquivalentTo(expectedReuslt);
    }

    [Test]
    public void SpeechPartFilter_Should_CorrectlyFilterOutSpecifiedParts()
    {
        var text = new[] { "кот", "коты", "котов" };
        var expectedReuslt = Array.Empty<string>();

        var myStem = new MyStem();
        var settings = ParseSettings(new SpeechPartFilterSettings(), "--add-parts", "S");
        var filter = new SpeechPartFilter(settings, myStem);

        var actualResult = text.Where(filter.IsValid);

        actualResult.Should().BeEquivalentTo(expectedReuslt);
    }
}
