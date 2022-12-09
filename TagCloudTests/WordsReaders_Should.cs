using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;
using TagCloudCreatorExtensions.WordsFileReaders;

namespace TagCloudTests;

[TestFixture]
public class WordsReaders_Should
{
    private const string TestText = "a bc\r\nde f";
    private IWordsPathSettingsProvider _pathSettingsProvider = null!;

    [SetUp]
    public void Setup()
    {
        var pathSettings = A.Fake<IWordsPathSettings>();
        _pathSettingsProvider = A.Fake<IWordsPathSettingsProvider>();
        A.CallTo(() => _pathSettingsProvider.GetWordsPathSettings())
            .Returns(pathSettings);
    }

    [Test]
    public void ReadTxtFile_Successfully()
    {
        const string filePath = "text.txt";
        var reader = new TxtFileReader(_pathSettingsProvider);
        _pathSettingsProvider.GetWordsPathSettings().WordsPath = filePath;

        using (var writer = new StreamWriter(File.Create(filePath)))
        {
            writer.Write(TestText);
        }

        reader.ReadFile().Should().Be(TestText);
        RemoveIfExists(filePath);
    }

    private static void RemoveIfExists(string file)
    {
        if (File.Exists(file))
            File.Delete(file);
    }
}