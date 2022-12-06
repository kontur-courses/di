using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure.Settings;

namespace TagCloudApp.Actions;

public class SaveSettingsAction : IUiAction
{
    private readonly SettingsManager _settingsManager;
    private readonly AppSettings _appSettings;

    public SaveSettingsAction(SettingsManager settingsManager, AppSettings appSettings)
    {
        _settingsManager = settingsManager;
        _appSettings = appSettings;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Save settings";
    public string Description => "Save settings";

    public void Perform()
    {
        _settingsManager.Save(_appSettings);
        MessageBox.Show("Saved successfully!", "Saved", MessageBoxButtons.OK);
    }
}