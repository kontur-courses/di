using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Domain;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Actions;

public class TagCloudPaintAction : IUiAction
{
    private readonly TagCloudPainter _painter;
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;
    private readonly IWordsFileReaderProvider _readersProvider;

    public TagCloudPaintAction(
        TagCloudPainter painter,
        IWordsPathSettingsProvider pathSettingsProvider,
        IWordsFileReaderProvider readersProvider
    )
    {
        _painter = painter;
        _pathSettingsProvider = pathSettingsProvider;
        _readersProvider = readersProvider;
    }

    public MenuCategory Category => MenuCategory.TagCloud;
    public string Name => "Paint";
    public string Description => "Paint tag cloud for words";


    private string? _filter;

    public void Perform()
    {
        var pathSettings = _pathSettingsProvider.GetWordsPathSettings();
        _filter ??= string.Join("|", _readersProvider.SupportedExtensions
            .Select(extension => $"{extension}|*{extension}")
        );
        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Filter = _filter,
            InitialDirectory = Path.GetFullPath(pathSettings.WordsPath)
        };
        var res = dialog.ShowDialog();
        if (res != DialogResult.OK)
            return;
        pathSettings.WordsPath = dialog.FileName;
        _painter.Paint();
    }
}