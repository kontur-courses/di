using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Actions;

public class TagCloudLayouterSettingsAction : IUiAction
{
    private readonly TagCloudLayouterSettings _layouterSettings;

    public TagCloudLayouterSettingsAction(TagCloudLayouterSettings layouterSettings)
    {
        _layouterSettings = layouterSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Layouter...";
    public string Description => "Cloud form factor and aspect ratio";

    public void Perform()
    {
        SettingsForm.For(_layouterSettings).ShowDialog();
    }
}