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
        var path = Path.GetTempFileName();
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
        var fakeStats = GetTextStats(text);

        var fakeReader = A.Fake<ITextReader>();
        A.CallTo(() => fakeReader.ReadLines()).Returns(text);

        var textAnalyzer = new AnalyzerWrapper(fakeReader, Array.Empty<IWordNormalizer>(), Array.Empty<IWordFilter>(), new[] { ' ', ',', '.' });

        var actualResult = textAnalyzer.AnalyzeText();

        actualResult.Statistics.Should().BeEquivalentTo(fakeStats.Statistics);
        actualResult.TotalWordCount.Should().Be(fakeStats.TotalWordCount);
    }

    private static ITextStats GetTextStats(string[] text)
    {
        var fakeStats = A.Fake<ITextStats>();
        A.CallTo(() => fakeStats.Statistics).Returns(text.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()));
        A.CallTo(() => fakeStats.TotalWordCount).Returns(text.Length);
        return fakeStats;
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

    [Test]
    public void TagPacker_Should_CorrectlyAssembleTags()
    {
        var text = new[] { "кот", "коты", "котов" };

        var fakeStats = GetTextStats(text);

        var fakeAnalyzer = A.Fake<ITextAnalyzer>();
        A.CallTo(() => fakeAnalyzer.AnalyzeText()).Returns(fakeStats);

        var fakeTags = fakeStats.Statistics.Select(x =>
        {
            var tag = A.Fake<ITag>();
            A.CallTo(() => tag.Value).Returns(x.Key);
            A.CallTo(() => tag.RelativeSize).Returns((double)x.Value / fakeStats.TotalWordCount);
            return tag;
        });

        var packer = new TagPacker(fakeAnalyzer);

        var actualResult = packer.GetTags();

        actualResult.Should().BeEquivalentTo(fakeTags);
    }

    private class AnalyzerWrapper : TextAnalyzer
    {
        public AnalyzerWrapper(ITextReader textReader, IWordNormalizer[] wordNormalizers, IWordFilter[] wordFilters, char[] wordSeparators) : base(textReader, wordNormalizers, wordFilters, wordSeparators)
        {
        }
    }
}
