using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Domain.Settings;

namespace TagCloudApp.Actions;

public class TagCloudLayouterSettingsAction : IUiAction
{
    private readonly WeightedTagCloudLayouterSettings _layouterSettings;

    public TagCloudLayouterSettingsAction(WeightedTagCloudLayouterSettings layouterSettings)
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