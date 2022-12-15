using FluentAssertions;
using TagCloud.Common.WeightCounter;

namespace TagCloud.Tests;

[TestFixture]
public class SimpleWeightCounter_Should
{
    private IWeightCounter counter;

    [SetUp]
    public void SetUp()
    {
        counter = new SimpleWeightCounter();
    }

    [Test]
    public void CountWeights_ShouldWorkCorrect_WithEmptyCollection()
    {
        var wordsWithWeights = counter.CountWeights(new List<string>());
        wordsWithWeights.Should().BeEmpty();
    }

    [TestCase("слово слово слово слово", 4, "слово")]
    [TestCase("слово", 1, "слово")]
    public void CountWeights_ShouldWorkCorrect_OneRepeatingWord(string line, int weight, string key)
    {
        var words = line.Split(' ');
        var wordsWithWeights = counter.CountWeights(words);
        wordsWithWeights.Count.Should().Be(1);
        wordsWithWeights[key].Should().Be(weight);
    }

    [TestCase("много слов и нет повторов")]
    [TestCase("два слова")]
    public void CountWeights_ShouldWorkCorrect_ManyNobRepeatingWords(string line)
    {
        var words = line.Split(' ');
        var wordsWithWeights = counter.CountWeights(words);
        wordsWithWeights.Count.Should().Be(words.Length);
        wordsWithWeights.All(pair => pair.Value == 1).Should().BeTrue();
    }

    [TestCase("повтор повтор слова повтор слова слова", 2)]
    [TestCase("повтор повтор слова слова слова повтор", 2)]
    [TestCase("день день день ночь ночь ночь утро утро утро", 3)]
    public void CountWeights_ShouldWorkCorrect_RepeatingWords(string line, int count)
    {
        var words = line.Split(' ');
        var wordsWithWeights = counter.CountWeights(words);
        wordsWithWeights.Count.Should().Be(count);
        wordsWithWeights.All(pair => pair.Value == 3).Should().BeTrue();
    }

    [TestCase("чай чайник чайка")]
    public void CountWeights_ShouldNotCount_WordsInsideWord(string line)
    {
        var words = line.Split(' ');
        var wordsWithWeights = counter.CountWeights(words);
        wordsWithWeights.All(pair => pair.Value == 1).Should().BeTrue();
        wordsWithWeights.Count.Should().NotBe(1);
    }
}