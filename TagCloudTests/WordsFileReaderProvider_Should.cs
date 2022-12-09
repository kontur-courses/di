using TagCloudCreator.Domain.Providers;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;
using TagCloudCreatorExtensions.ImageSavers;
using TagCloudCreatorExtensions.WordsFileReaders;

namespace TagCloudTests;

[TestFixture]
public class WordsFileReaderProvider_Should
{
    private IFileReaderProvider _readerProvider = null!;
    private IWordsPathSettings _pathSettings = null!;

    [SetUp]
    public void Setup()
    {
        _pathSettings = A.Fake<IWordsPathSettings>();
        var pathSettingsProvider = A.Fake<IWordsPathSettingsProvider>();
        A.CallTo(() => pathSettingsProvider.GetWordsPathSettings())
            .Returns(_pathSettings);

        _readerProvider = new FileReaderProvider(
            new IFileReader[]
            {
                new TxtFileReader(pathSettingsProvider),
                new DocxFileReader(pathSettingsProvider)
            },
            pathSettingsProvider
        );
    }

    [TestCase(".txt", typeof(TxtFileReader), TestName = "Txt")]
    [TestCase(".docx", typeof(DocxFileReader), TestName = "Docx")]
    public void ReturnCorrectReader_ForCorrectExtension(string extension, Type expectedReaderType)
    {
        _pathSettings.WordsPath = $"text{extension}";
        _readerProvider.GetReader().GetType().Should().Be(expectedReaderType);
    }

    [Test]
    public void ThrowOperationException_ForBadExtension()
    {
        _pathSettings.WordsPath = "text.abc";
        _readerProvider.Invoking(provider => provider.GetReader())
            .Should().Throw<InvalidOperationException>()
            .Which.Message.Should().Contain(".abc");
    }
}