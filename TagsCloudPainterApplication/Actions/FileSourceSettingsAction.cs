using TagsCloudPainterApplication.Infrastructure.Settings;
using TagsCloudPainterApplication.Infrastructure.Settings.FilesSource;

namespace TagsCloudPainterApplication.Actions;

public class FileSourceSettingsAction : IUiAction
{
    private readonly IFilesSourceSettings filesSourceSettings;

    public FileSourceSettingsAction(IFilesSourceSettings filesSourceSettings)
    {
        this.filesSourceSettings = filesSourceSettings ?? throw new ArgumentNullException(nameof(filesSourceSettings));
    }

    public string Category => "Настройки";
    public string Name => "Ресурсы";
    public string Description => "Укажите ресурсы";

    public void Perform()
    {
        SettingsForm.For(filesSourceSettings).ShowDialog();
    }
}