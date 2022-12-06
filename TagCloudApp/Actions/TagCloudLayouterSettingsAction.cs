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
    public string Name => "Алгоритм...";
    public string Description => "Форм фактор облака, соотношение сторон";

    public void Perform()
    {
        SettingsForm.For(_layouterSettings).ShowDialog();
    }
}