using CircularCloudLayouter;
using CircularCloudLayouter.WeightedLayouter;
using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Actions;

public class TagCloudPaintAction : IUiAction
{
    private readonly TagCloudLayouterSettings _settings;
    private readonly Func<ITagCloudLayouter, TagCloudPainter> _painterFactory;
    private readonly WordsFilePathProvider _wordsFilePathProvider;
    private readonly ImageSettings _imageSettings;

    public TagCloudPaintAction(
        Func<ITagCloudLayouter, TagCloudPainter> painterFactory,
        TagCloudLayouterSettings settings,
        WordsFilePathProvider wordsFilePathProvider, ImageSettings imageSettings)
    {
        _settings = settings;
        _painterFactory = painterFactory;
        _wordsFilePathProvider = wordsFilePathProvider;
        _imageSettings = imageSettings;
    }

    public MenuCategory Category => MenuCategory.TagCloud;
    public string Name => "Создать облако тегов";
    public string Description => "Создание облаков с настройкой";

    public void Perform()
    {
        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            DefaultExt = "txt",
            InitialDirectory = _wordsFilePathProvider.Path,
            Filter = "Текст (*.txt)|*.txt"
        };
        var res = dialog.ShowDialog();
        if (res != DialogResult.OK)
            return;
        _wordsFilePathProvider.Path = dialog.FileName;
        // SettingsForm.For(_settings).ShowDialog();
        var layouter = new WeightedTagCloudLayouter(new Point(_imageSettings.Width / 2, _imageSettings.Height / 2),
            _settings.FormFactor.WithRatio(_settings.WidthToHeightRatio));

        _painterFactory(layouter).Paint();
    }
}