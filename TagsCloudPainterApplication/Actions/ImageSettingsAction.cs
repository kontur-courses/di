using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication.Actions;

public class ImageSettingsAction : IUiAction
{
    private readonly ImageSettings imageSettings;

    public ImageSettingsAction(ImageSettings imageSettings)
    {
        this.imageSettings = imageSettings ?? throw new ArgumentNullException(nameof(imageSettings));
    }

    public string Category => "Настройки";
    public string Name => "Изображение";
    public string Description => "Укажите размер изображения";

    public void Perform()
    {
        SettingsForm.For(imageSettings).ShowDialog();
    }
}