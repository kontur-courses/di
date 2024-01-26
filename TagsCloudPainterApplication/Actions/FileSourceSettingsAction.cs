using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication.Actions;

public class FileSourceSettingsAction : IUiAction
{
    private readonly FilesSourceSettings filesSourceSettings;

    public FileSourceSettingsAction(FilesSourceSettings filesSourceSettings)
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