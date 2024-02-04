using Newtonsoft.Json;
using TagsCloudContainer.SettingsClasses;

namespace WinFormsApp.SettingsManager
{
    public static class SettingsManager
    {
        private const string settingsFile = "settings.json";
        public static void SaveSettings(AppSettings settings, string filePath = settingsFile)
        {
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(settings));
        }

        public static AppSettings LoadSettings(string filePath = settingsFile)
        {
            AppSettings settings = null;
            if (File.Exists(filePath))
            {
                var serialized = File.ReadAllText(filePath);
                settings = JsonConvert.DeserializeObject<AppSettings>(serialized);
            }
            else
            {
                settings = new AppSettings();
                settings.DrawingSettings = new CloudDrawingSettings();
                SaveSettings(settings);
            }
            return settings;
        }
    }
}