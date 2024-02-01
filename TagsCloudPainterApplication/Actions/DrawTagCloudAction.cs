using TagsCloudPainter;
using TagsCloudPainter.CloudLayouter;
using TagsCloudPainter.Drawer;
using TagsCloudPainter.FileReader;
using TagsCloudPainter.Parser;
using TagsCloudPainter.Tags;
using TagsCloudPainterApplication.Infrastructure;
using TagsCloudPainterApplication.Infrastructure.Settings;
using TagsCloudPainterApplication.Infrastructure.Settings.FilesSource;
using TagsCloudPainterApplication.Infrastructure.Settings.Image;
using TagsCloudPainterApplication.Infrastructure.Settings.TagsCloud;

namespace TagsCloudPainterApplication.Actions;

public class DrawTagCloudAction : IUiAction
{
    private readonly ICloudDrawer cloudDrawer;
    private readonly ICloudLayouter cloudLayouter;
    private readonly IFilesSourceSettings filesSourceSettings;
    private readonly IImageHolder imageHolder;
    private readonly IImageSettings imageSettings;
    private readonly Palette palette;
    private readonly ITagsBuilder tagsBuilder;
    private readonly ITagsCloudSettings tagsCloudSettings;
    private readonly IFormatFileReader<string> textFileReader;
    private readonly ITextParser textParser;

    public DrawTagCloudAction(
        IImageSettings imageSettings,
        ITagsCloudSettings tagsCloudSettings,
        IFilesSourceSettings filesSourceSettings,
        IImageHolder imageHolder,
        ICloudDrawer cloudDrawer,
        ICloudLayouter cloudLayouter,
        ITagsBuilder tagsBuilder,
        ITextParser textParser,
        IFormatFileReader<string> textFileReader,
        Palette palette)
    {
        this.cloudDrawer = cloudDrawer ?? throw new ArgumentNullException(nameof(cloudDrawer));
        this.cloudLayouter = cloudLayouter ?? throw new ArgumentNullException(nameof(cloudLayouter));
        this.tagsBuilder = tagsBuilder ?? throw new ArgumentNullException(nameof(tagsBuilder));
        this.textParser = textParser ?? throw new ArgumentNullException(nameof(textParser));
        this.textFileReader = textFileReader ?? throw new ArgumentNullException(nameof(textFileReader));
        this.imageSettings = imageSettings ?? throw new ArgumentNullException(nameof(imageSettings));
        this.tagsCloudSettings = tagsCloudSettings ?? throw new ArgumentNullException(nameof(tagsCloudSettings));
        this.imageHolder = imageHolder ?? throw new ArgumentNullException(nameof(imageHolder));
        this.filesSourceSettings = filesSourceSettings ?? throw new ArgumentNullException(nameof(filesSourceSettings));
        this.palette = palette ?? throw new ArgumentNullException(nameof(palette));
    }

    public string Category => "Облако тэгов";

    public string Name => "Нарисовать";

    public string Description => "Нарисовать облако тэгов";

    public void Perform()
    {
        var wordsFilePath = GetFilePath();
        if (string.IsNullOrEmpty(wordsFilePath))
            return;

        SettingsForm.For(tagsCloudSettings).ShowDialog();
        tagsCloudSettings.CloudSettings.BackgroundColor = palette.BackgroundColor;
        tagsCloudSettings.TagSettings.TagColor = palette.PrimaryColor;

        var wordsText = textFileReader.ReadFile(wordsFilePath);
        tagsCloudSettings.TextSettings.BoringText = textFileReader.ReadFile(filesSourceSettings.BoringTextFilePath);
        var parsedWords = textParser.ParseText(wordsText);
        var cloud = GetCloud(parsedWords);
        DrawCloud(cloud);
    }

    private static string GetFilePath()
    {
        OpenFileDialog fileDialog = new()
        {
            InitialDirectory = Environment.CurrentDirectory,
            Filter =
                "Текстовый файл txt (*.txt)|*.txt|Текстовый файл doc (*.doc)|*.doc|Текстовый файл docx (*.docx)|*.docx",
            FilterIndex = 0,
            RestoreDirectory = true
        };
        fileDialog.ShowDialog();
        return fileDialog.FileName;
    }

    private TagsCloud GetCloud(List<string> words)
    {
        var tags = tagsBuilder.GetTags(words);
        cloudLayouter.Reset();
        cloudLayouter.PutTags(tags);
        var cloud = cloudLayouter.GetCloud();
        return cloud;
    }

    private void DrawCloud(TagsCloud cloud)
    {
        using var bitmap = cloudDrawer.DrawCloud(cloud, imageSettings.Width, imageSettings.Height);
        using (var graphics = imageHolder.StartDrawing())
        {
            graphics.DrawImage(bitmap, new Point(0, 0));
        }

        imageHolder.UpdateUi();
    }
}