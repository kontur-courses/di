using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;
using TagCloudCreatorExtensions.WordsFileReaders;
using Text = DocumentFormat.OpenXml.Drawing.Text;

namespace TagCloudTests;

[TestFixture]
public class WordsReaders_Should
{
    private const string TestText = "a bc\r\nde f";
    private IWordsPathSettingsProvider _pathSettingsProvider = null!;
    private string? _filePathToRemove;

    [SetUp]
    public void Setup()
    {
        var pathSettings = A.Fake<IWordsPathSettings>();
        _pathSettingsProvider = A.Fake<IWordsPathSettingsProvider>();
        A.CallTo(() => _pathSettingsProvider.GetWordsPathSettings())
            .Returns(pathSettings);
    }

    [TearDown]
    public void TearDown()
    {
        if (_filePathToRemove is not null && File.Exists(_filePathToRemove))
            File.Delete(_filePathToRemove);
    }

    [Test]
    public void ReadTxtFile_Successfully()
    {
        _filePathToRemove = "text.txt";
        using (var writer = new StreamWriter(File.Create(_filePathToRemove)))
        {
            writer.Write(TestText);
        }

        Check(new TxtFileReader(_pathSettingsProvider));
    }

    [Test]
    public void ReadDocxFile_Successfully()
    {
        _filePathToRemove = "text.docx";
        using var wordDoc = WordprocessingDocument.Create(_filePathToRemove, WordprocessingDocumentType.Document);
        {
            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());
            var body = mainPart.Document.Body!;
            foreach (var line in TestText.Split("\r\n"))
                body.AppendChild(new Text(line));

            wordDoc.Close();
        }

        Check(new DocxFileReader(_pathSettingsProvider));
    }

    private void Check(IFileReader reader)
    {
        _pathSettingsProvider.GetWordsPathSettings().WordsPath = _filePathToRemove!;

        reader.ReadFile().Should().Be(TestText);
    }
}