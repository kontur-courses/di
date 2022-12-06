using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Actions;

public class TagCloudPaintAction : IUiAction
{
    private readonly TagCloudPainter _painter;
    private readonly IWordsPathProvider _wordsPathProvider;
    private readonly IWordsFileReaderProvider _readersProvider;

    public TagCloudPaintAction(
        TagCloudPainter painter,
        IWordsPathProvider wordsPathProvider,
        IWordsFileReaderProvider readersProvider
    )
    {
        _painter = painter;
        _wordsPathProvider = wordsPathProvider;
        _readersProvider = readersProvider;
    }

    public MenuCategory Category => MenuCategory.TagCloud;
    public string Name => "Paint";
    public string Description => "Paint tag cloud for words";


    private string? _filter;

    public void Perform()
    {
        _filter ??= string.Join("|", _readersProvider.SupportedExtensions
            .Select(extension => $"{extension}|*{extension}")
        );

        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Filter = _filter,
            InitialDirectory = Path.GetFullPath(_wordsPathProvider.WordsPath)
        };
        var res = dialog.ShowDialog();
        if (res != DialogResult.OK)
            return;
        _wordsPathProvider.WordsPath = dialog.FileName;
        _painter.Paint();
    }
}