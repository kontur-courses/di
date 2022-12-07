using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Domain;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudApp.Actions;

public class TagCloudPaintAction : IUiAction
{
    private readonly TagCloudPainter _painter;
    private readonly IWordsPathSettings _wordsPathSettings;
    private readonly IWordsFileReaderProvider _readersProvider;

    public TagCloudPaintAction(
        TagCloudPainter painter,
        IWordsPathSettings wordsPathSettings,
        IWordsFileReaderProvider readersProvider
    )
    {
        _painter = painter;
        _wordsPathSettings = wordsPathSettings;
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
            InitialDirectory = Path.GetFullPath(_wordsPathSettings.WordsPath)
        };
        var res = dialog.ShowDialog();
        if (res != DialogResult.OK)
            return;
        _wordsPathSettings.WordsPath = dialog.FileName;
        _painter.Paint();
    }
}