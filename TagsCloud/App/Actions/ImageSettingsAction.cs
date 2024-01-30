using TagsCloud.App.Settings;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.App.Actions;

public class ImageSettingsAction : IUiAction
{
    private readonly IImageHolder imageHolder;
    private readonly ImageSettings imageSettings;

    public ImageSettingsAction(IImageHolder imageHolder,
        ImageSettings imageSettings)
    {
        this.imageHolder = imageHolder;
        this.imageSettings = imageSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Изображение...";
    public string Description => "Размеры изображения";

    public void Perform()
    {
        SettingsForm.For(imageSettings).ShowDialog();
        imageHolder.RecreateImage(imageSettings);
    }
}