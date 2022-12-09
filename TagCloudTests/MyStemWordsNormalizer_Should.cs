using MyStemWrapper;
using TagCloudCreatorExtensions;

namespace TagCloudTests;

[TestFixture]
public class MyStemWordsNormalizer_Should
{
    private const string MyStemExecutablePath = "../../../../MyStemBin/mystem.exe";
    private MyStemWordsNormalizer _normalizer = null!;

    [SetUp]
    public void SetUp()
    {
        _normalizer = new MyStemWordsNormalizer(new MyStemWordsGrammarInfoParser(MyStemExecutablePath));
    }

    [Test]
    public void ReturnWordsInOriginalForm_Successfully()
    {
        _normalizer.GetWordsOriginalForm("коровы едят траву")
            .Should().BeEquivalentTo("корова", "есть", "трава");
    }

    [TestCase("", TestName = "Empty")]
    [TestCase("123", TestName = "Digits")]
    [TestCase("@ $ %", TestName = "Symbols")]
    public void ReturnNothing_ForStringWithoutWords(string str)
    {
        _normalizer.GetWordsOriginalForm(str)
            .Should().BeEmpty();
    }
}