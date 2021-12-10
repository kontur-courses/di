using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Infrastructure.WordProcessing;

internal class FilterTests
{
    [Test]
    public void FilterWords_ShouldReturnCorrectEnumerable_WhenThereIsNoConditionsAndInputAreStrings()
    {
        var filter = new Filter();

        var actual = filter.FilterWords(FilterTestCases.Strings);

        actual.Should().BeEquivalentTo(FilterTestCases.Strings);
    }

    [Test]
    public void FilterWords_ShouldReturnCorrectEnumerable_WhenThereIsNoConditionsAndInputAreLemmas()
    {
        var filter = new Filter();

        var actual = filter.FilterWords(FilterTestCases.Lemmas);

        actual.Should().BeEquivalentTo(FilterTestCases.Lemmas.Select(x => x.Word));
    }

    [TestCaseSource(typeof(FilterTestCases), nameof(FilterTestCases.FilterStringTestCaseDatas))]
    public void FilterWords(List<string> words, List<string> expected)
    {
        var filter = new Filter().AddCondition(x => x.Length > 1);

        var actual = filter.FilterWords(words);

        actual.Should().BeEquivalentTo(expected);
    }

    [TestCaseSource(typeof(FilterTestCases), nameof(FilterTestCases.FilterLemmaTestCaseDatas))]
    public void FilterWords(List<Lemma> words, List<string> expected)
    {
        var filter = new Filter()
            .AddCondition(lemma => lemma.PartOfSpeech == PartOfSpeech.Noun)
            .AddCondition(x => x.Length > 1);

        var actual = filter.FilterWords(words);

        actual.Should().BeEquivalentTo(expected);
    }
}