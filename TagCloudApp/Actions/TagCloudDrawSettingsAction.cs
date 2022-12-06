using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Actions;

public class TagCloudDrawSettingsAction : IUiAction
{
    private readonly TagCloudPaintSettings _paintSettings;

    public TagCloudDrawSettingsAction(TagCloudPaintSettings paintSettings)
    {
        _paintSettings = paintSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Font and colors...";
    public string Description => "Font size, style and colors";

    public void Perform()
    {
        SettingsForm.For(_paintSettings).ShowDialog();
    }
}