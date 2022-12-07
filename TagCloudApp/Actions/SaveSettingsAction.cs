using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Infrastructure.Settings;

namespace TagCloudApp.Actions;

public class SaveSettingsAction : IUiAction
{
    private readonly SettingsManager _settingsManager;

    public SaveSettingsAction(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Save settings";
    public string Description => "Save settings";

    public void Perform()
    {
        _settingsManager.Save();
        MessageBox.Show("Saved successfully!", "Saved", MessageBoxButtons.OK);
    }
}