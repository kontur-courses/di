using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Domain;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Actions;

public class TagCloudPaintAction : IUiAction
{
    private readonly TagCloudPainter _painter;
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;
    private readonly IFileReaderProvider _readersProvider;

    public TagCloudPaintAction(
        TagCloudPainter painter,
        IWordsPathSettingsProvider pathSettingsProvider,
        IFileReaderProvider readersProvider
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
        var initialDirectory = Path.GetFullPath(pathSettings.WordsPath);
        if (File.Exists(initialDirectory))
            initialDirectory = new FileInfo(initialDirectory).Directory!.FullName;
        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Filter = _filter,
            InitialDirectory = initialDirectory
        };
        var res = dialog.ShowDialog();
        if (res != DialogResult.OK)
            return;
        pathSettings.WordsPath = dialog.FileName;
        _painter.Paint();
    }
}