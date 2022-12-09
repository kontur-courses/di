using MyStemWrapper;
using MyStemWrapper.Domain;

namespace TagCloudTests;

[TestFixture]
public class MyStemWordsGrammarInfoParser_Should
{
    private const string MyStemExecutablePath = "../../../../MyStemBin/mystem.exe";
    private MyStemWordsGrammarInfoParser _grammarInfoParser = null!;

    [SetUp]
    public void SetUp()
    {
        _grammarInfoParser = new MyStemWordsGrammarInfoParser(MyStemExecutablePath);
    }

    [TestCase(TestName = "Empty")]
    [TestCase("", TestName = "Empty string")]
    [TestCase("123", TestName = "String with only digits")]
    [TestCase("@ $ %", TestName = "Symbols")]
    public void ReturnNothing_ForBadSource(params string[] source)
    {
        _grammarInfoParser.Parse(source)
            .Should().BeEmpty();
    }

    [Test]
    public void ReturnCorrect_ForSingleWord()
    {
        _grammarInfoParser.Parse(new[] {"ананасов"})
            .Should().ContainSingle()
            .Which.Should().BeEquivalentTo(new WordGrammarInfo(
                "ананасов",
                "ананас",
                SpeechPart.Noun
            ));
    }

    [TestCase("абв", SpeechPart.Undefined, TestName = "Undefined")]
    [TestCase("синий", SpeechPart.Adjective, TestName = "Adjective")]
    [TestCase("быстро", SpeechPart.Adverb, TestName = "Adverb")]
    [TestCase("туда", SpeechPart.PronominalAdverb, TestName = "Pronominal adverb")]
    [TestCase("третий", SpeechPart.NumeralAdjective, TestName = "Numeral adjective")]
    [TestCase("чей", SpeechPart.PronominalAdjective, TestName = "Pronominal adjective")]
    [TestCase("и", SpeechPart.Union, TestName = "Union")]
    [TestCase("ах", SpeechPart.Interjection, TestName = "Interjection")]
    [TestCase("три", SpeechPart.Numeral, TestName = "Numeral")]
    [TestCase("лишь", SpeechPart.Particle, TestName = "Particle")]
    [TestCase("до", SpeechPart.Preposition, TestName = "Preposition")]
    [TestCase("чай", SpeechPart.Noun, TestName = "Noun")]
    [TestCase("он", SpeechPart.Pronoun, TestName = "Pronoun")]
    [TestCase("бежать", SpeechPart.Verb, TestName = "Verb")]
    public void ReturnCorrectSpeechPartForWords(string word, SpeechPart expected)
    {
        _grammarInfoParser.Parse(new[] {word})
            .Should().ContainSingle()
            .Which.SpeechPart.Should().Be(expected);
    }

    [Test]
    public void ReturnCorrect_ForSomeWordsInOneString()
    {
        _grammarInfoParser.Parse(new[] {"коровы едят траву"})
            .Should().BeEquivalentTo(new[]
                {
                    new WordGrammarInfo("коровы", "корова", SpeechPart.Noun),
                    new WordGrammarInfo("едят", "есть", SpeechPart.Verb),
                    new WordGrammarInfo("траву", "трава", SpeechPart.Noun)
                }
            );
    }

    [Test]
    public void ReturnCorrect_ForSomeWordsInDifferentString()
    {
        _grammarInfoParser.Parse(new[] {"коровы", "едят", "траву"})
            .Should().BeEquivalentTo(new[]
                {
                    new WordGrammarInfo("коровы", "корова", SpeechPart.Noun),
                    new WordGrammarInfo("едят", "есть", SpeechPart.Verb),
                    new WordGrammarInfo("траву", "трава", SpeechPart.Noun)
                }
            );
    }
}