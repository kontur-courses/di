using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreatorExtensions.WordsFilters.Settings;

namespace TagCloudApp.Actions;

public class WordFiltersSettingsAction : IUiAction
{
    private readonly FiltersSettings _filtersSettings;

    public WordFiltersSettingsAction(
        FiltersSettings filtersSettings
    )
    {
        _filtersSettings = filtersSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Words filters...";
    public string Description => "Words filters configuration";

    public void Perform()
    {
        SettingsForm.For(_filtersSettings).ShowDialog();
    }
}