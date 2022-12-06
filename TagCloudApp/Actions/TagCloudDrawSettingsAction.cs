using TagCloudApp.Domain;

namespace TagCloudApp.Actions;

public class TagCloudDrawSettingsAction : IUiAction
{
    private readonly TagCloudDrawSettings _drawSettings;

    public TagCloudDrawSettingsAction(TagCloudDrawSettings drawSettings)
    {
        _drawSettings = drawSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Шрифт и цвета...";
    public string Description => "Цвет, шрифт, размер шрифта";

    public void Perform()
    {
        SettingsForm.For(_drawSettings).ShowDialog();
    }
}