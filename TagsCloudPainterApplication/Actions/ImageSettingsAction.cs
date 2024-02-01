using TagsCloudPainterApplication.Infrastructure.Settings;
using TagsCloudPainterApplication.Infrastructure.Settings.Image;

namespace TagsCloudPainterApplication.Actions;

public class ImageSettingsAction : IUiAction
{
    private readonly IImageSettings imageSettings;

    public ImageSettingsAction(IImageSettings imageSettings)
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