using TagsCloudVisualization.Common;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

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
        var dialog = SettingsForm.For(imageSettings).ShowDialog();
        if (dialog == DialogResult.OK)
            imageHolder.RecreateImage(imageSettings);
    }
}